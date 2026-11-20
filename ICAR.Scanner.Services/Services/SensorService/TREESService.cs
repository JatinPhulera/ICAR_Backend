using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.DataAccess.Repository;
using ICAR.Scanner.Models.DTOs;
using AutoMapper;

namespace ICAR.Scanner.Services.Services.TreeService;

public class TREESService : ITREESService
{
    private readonly IRepository<Tree> _treeRepository;
    private readonly IRepository<SENSOR> _sensorRepository;
    private readonly IRepository<AuditTree> _auditTreeRepository;
    private readonly IRepository<FileDetail> _fileDetailRepository;
    private readonly IMapper _mapper;

    public TREESService(
        IRepository<Tree> treeRepository,
        IRepository<SENSOR> sensorRepository,
        IRepository<AuditTree> auditTreeRepository,
        IRepository<FileDetail> fileDetailRepository,
        IMapper mapper)
    {
        _treeRepository = treeRepository;
        _sensorRepository = sensorRepository;
        _auditTreeRepository = auditTreeRepository;
        _fileDetailRepository = fileDetailRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TreesDto>> GetAllTreeAsync()
    {
        var trees = await _treeRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TreesDto>>(trees.Where(t => t.IsActive != false).OrderByDescending(t => t.CreatedOn));
    }

    public async Task<TreesDto?> GetTreeByIdAsync(Guid TreeId)
    {
        var tree = await _treeRepository.GetByIdAsync(TreeId);
        return tree == null ? null : _mapper.Map<TreesDto>(tree);
    }

    public async Task<TreesDto?> GetTreeByRfidAsync(string rfid)
    {
        var sensors = await _sensorRepository.GetAllAsync();
        var sensor = sensors.FirstOrDefault(s => string.Equals(s.SensorUID, rfid, StringComparison.OrdinalIgnoreCase));
        if (sensor == null)
        {
            // Also fallback to checking SensorID (Alias) just in case
            sensor = sensors.FirstOrDefault(s => string.Equals(s.SensorID, rfid, StringComparison.OrdinalIgnoreCase));
            if (sensor == null) return null;
        }

        var trees = await _treeRepository.GetAllAsync();
        var tree = trees.FirstOrDefault(t => t.SENSORID == sensor.Id);
        return tree == null ? null : _mapper.Map<TreesDto>(tree);
    }

    public async Task<TreesDto> CreateTreeAsync(TREESCreateDTO treeCreateDto)
    {
        // Validate the sensor (no writes yet) before touching the tree — writing the
        // "assigned" flag here and then failing to save the tree would orphan the sensor
        // as permanently assigned with no tree, blocking it from ever being used again.
        SENSOR? sensorToAssign = null;
        if (treeCreateDto.SensorId.HasValue)
        {
            sensorToAssign = await ValidateSensorForAssignmentAsync(treeCreateDto.SensorId.Value);
        }

        var tree = _mapper.Map<Tree>(treeCreateDto);
        tree.Id = Guid.NewGuid();
        tree.CreatedOn = DateTime.UtcNow;
        tree.CreatedBy = string.IsNullOrEmpty(treeCreateDto.CreatedBy) ? treeCreateDto.AddedBy : treeCreateDto.CreatedBy;
        tree.UpdatedBy = string.IsNullOrEmpty(treeCreateDto.UpdatedBy) ? tree.CreatedBy : treeCreateDto.UpdatedBy;
        tree.BotanicalName = string.IsNullOrEmpty(tree.BotanicalName) ? "Unknown" : tree.BotanicalName;
        tree.OperatorName = string.IsNullOrEmpty(tree.OperatorName) ? tree.AddedByName : tree.OperatorName;
        tree.IsActive = true;

        await _treeRepository.AddAsync(tree);

        if (sensorToAssign != null)
        {
            sensorToAssign.IsAssigned = true;
            sensorToAssign.UpdatedOn = DateTime.UtcNow;
            await _sensorRepository.UpdateAsync(sensorToAssign);
        }

        return _mapper.Map<TreesDto>(tree);
    }

    public async Task<bool> UpdateTreeAsync(TreesDto treeDto, IReadOnlySet<string>? providedKeys = null)
    {
        var tree = await _treeRepository.GetByIdAsync(treeDto.Id);
        if (tree == null) return false;

        // providedKeys (from the raw request JSON) tells us which fields the client
        // actually sent vs. which are just C# defaults for keys it never included.
        // Without it (e.g. an internal caller), assume everything was provided —
        // matches the old full-overwrite behavior for anyone not passing it.
        bool sensorIdProvided = providedKeys == null || providedKeys.Contains(nameof(Tree.SENSORID));

        var previousSensorId = tree.SENSORID;
        var newSensorId = sensorIdProvided ? treeDto.SENSORID : previousSensorId;
        var sensorChanged = sensorIdProvided && newSensorId != previousSensorId;

        // Validate the new sensor (no writes yet) before saving the tree — see CreateTreeAsync
        // for why writing the "assigned" flag before the tree save is committed is unsafe.
        SENSOR? sensorToAssign = null;
        if (sensorChanged && newSensorId.HasValue)
        {
            sensorToAssign = await ValidateSensorForAssignmentAsync(newSensorId.Value);
        }

        treeDto.CreatedOn = tree.CreatedOn;
        treeDto.CreatedBy = tree.CreatedBy;
        _mapper.Map(treeDto, tree, opts => opts.Items["ProvidedKeys"] = providedKeys);
        if (sensorIdProvided)
        {
            tree.SENSORID = newSensorId;
        }
        tree.IsActive = treeDto.IsActive ?? true;
        tree.UpdatedOn = DateTime.UtcNow;
        await _treeRepository.UpdateAsync(tree);

        if (sensorChanged)
        {
            if (sensorToAssign != null)
            {
                sensorToAssign.IsAssigned = true;
                sensorToAssign.UpdatedOn = DateTime.UtcNow;
                await _sensorRepository.UpdateAsync(sensorToAssign);
            }

            if (previousSensorId.HasValue && previousSensorId != newSensorId)
            {
                await UnassignSensorAsync(previousSensorId.Value);
            }
        }

        return true;
    }

    public async Task<bool> DeleteTreeAsync(Guid TreeId)
    {
        var tree = await _treeRepository.GetByIdAsync(TreeId);
        if (tree == null) return false;

        // Remove dependent audit/file rows first — their FK to Tree is required (non-nullable),
        // so leaving them in place makes the delete below fail with a FK constraint violation.
        var audits = await _auditTreeRepository.GetAllAsync();
        foreach (var audit in audits.Where(a => a.TreeId == TreeId).ToList())
        {
            await _auditTreeRepository.DeleteAsync(audit);
        }

        var docs = await _fileDetailRepository.GetAllAsync();
        foreach (var doc in docs.Where(d => d.TreeId == TreeId).ToList())
        {
            await _fileDetailRepository.DeleteAsync(doc);
        }

        if (tree.SENSORID.HasValue)
        {
            await UnassignSensorAsync(tree.SENSORID.Value);
        }

        await _treeRepository.DeleteAsync(tree);
        return true;
    }

    /// <summary>Validates a sensor is eligible for assignment without writing anything.</summary>
    private async Task<SENSOR> ValidateSensorForAssignmentAsync(Guid sensorId)
    {
        var sensor = await _sensorRepository.GetByIdAsync(sensorId)
            ?? throw new InvalidOperationException("Sensor not found.");

        if (sensor.IsAssigned)
        {
            throw new InvalidOperationException("Sensor is already assigned to another tree.");
        }

        if (sensor.IsActive != true)
        {
            throw new InvalidOperationException("Sensor is not active.");
        }

        return sensor;
    }

    private async Task UnassignSensorAsync(Guid sensorId)
    {
        var sensor = await _sensorRepository.GetByIdAsync(sensorId);
        if (sensor == null) return;

        sensor.IsAssigned = false;
        sensor.UpdatedOn = DateTime.UtcNow;
        await _sensorRepository.UpdateAsync(sensor);
    }

    //TODO:: JP Need to move this to helper with proper Hashing
    private string HashPassword(string password)
    {
        // Replace with a secure hash in production!
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
    }
}



