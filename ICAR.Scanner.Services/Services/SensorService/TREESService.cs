using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.DataAccess.Repository;
using ICAR.Scanner.Models.DTOs;
using AutoMapper;

namespace ICAR.Scanner.Services.Services.TreeService;

public class TREESService : ITREESService
{
    private readonly IRepository<Tree> _treeRepository;
    private readonly IRepository<SENSOR> _sensorRepository;
    private readonly IMapper _mapper;

    public TREESService(IRepository<Tree> treeRepository, IRepository<SENSOR> sensorRepository, IMapper mapper)
    {
        _treeRepository = treeRepository;
        _sensorRepository = sensorRepository;
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
        if (treeCreateDto.SensorId.HasValue)
        {
            await AssignSensorAsync(treeCreateDto.SensorId.Value);
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

        return _mapper.Map<TreesDto>(tree);
    }

    public async Task<bool> UpdateTreeAsync(TreesDto treeDto)
    {
        var tree = await _treeRepository.GetByIdAsync(treeDto.Id);
        if (tree == null) return false;

        var previousSensorId = tree.SENSORID;
        var newSensorId = treeDto.SENSORID;

        if (newSensorId != previousSensorId)
        {
            if (newSensorId.HasValue)
            {
                await AssignSensorAsync(newSensorId.Value);
            }

            if (previousSensorId.HasValue && previousSensorId != newSensorId)
            {
                await UnassignSensorAsync(previousSensorId.Value);
            }
        }

        treeDto.CreatedOn = tree.CreatedOn;
        treeDto.CreatedBy = tree.CreatedBy;
        if (!treeDto.SensorTypeId.HasValue && tree.SensorTypeId.HasValue)
        {
            treeDto.SensorTypeId = tree.SensorTypeId;
        }
        _mapper.Map(treeDto, tree);
        tree.IsActive = treeDto.IsActive ?? true;
        tree.UpdatedOn = DateTime.UtcNow;
        await _treeRepository.UpdateAsync(tree);
        return true;
    }

    public async Task<bool> DeleteTreeAsync(Guid TreeId)
    {
        var tree = await _treeRepository.GetByIdAsync(TreeId);
        if (tree == null) return false;

        if (tree.SENSORID.HasValue)
        {
            await UnassignSensorAsync(tree.SENSORID.Value);
        }

        await _treeRepository.DeleteAsync(tree);
        return true;
    }

    private async Task AssignSensorAsync(Guid sensorId)
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

        sensor.IsAssigned = true;
        sensor.UpdatedOn = DateTime.UtcNow;
        await _sensorRepository.UpdateAsync(sensor);
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



