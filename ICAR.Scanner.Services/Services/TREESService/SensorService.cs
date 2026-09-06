using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.DataAccess.Repository;
using ICAR.Scanner.Models.DTOs;
using AutoMapper;
using ICAR.Scanner.Models.DTOs.Request;
using Microsoft.EntityFrameworkCore;

namespace ICAR.Scanner.Services.Services.SensorService;

public class SensorService : ISensorService
{
        private readonly IRepository<SENSOR> _sensorRepository;
        private readonly IRepository<Tree> _treeRepository;
        private readonly IMapper _mapper;

        public SensorService(
            IRepository<SENSOR> sensorRepository,
            IRepository<Tree> treeRepository,
            IMapper mapper)
        {
            _sensorRepository = sensorRepository;
            _treeRepository   = treeRepository;
            _mapper           = mapper;
        }

        public async Task<IEnumerable<SensorDTO>> GetAllSensorsAsync()
        {
            var sensors = await _sensorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SensorDTO>>(sensors.Where(s => s.IsActive == true).OrderByDescending(s => s.CreatedOn));
        }

        public async Task<IEnumerable<SENSOR>> GetUnassignedActiveSensorsAsync()
        {
            var sensors = await _sensorRepository.GetAllAsync();
            return sensors.Where(s => s.IsActive == true && !s.IsAssigned).OrderByDescending(s => s.CreatedOn);
        }

        public async Task<SensorDTO?> GetSensorByIdAsync(Guid sensorID)
        {
            var sensor = await _sensorRepository.GetByIdAsync(sensorID);
            return sensor == null ? null : _mapper.Map<SensorDTO>(sensor);
        }

        public async Task<SensorDTO> CreateSensorAsync(SensorCreateDTO sensorCreateDto)
        {
            // Reject duplicate RFID: SensorUID must be unique across all sensors
            if (!string.IsNullOrWhiteSpace(sensorCreateDto.SensorUID))
            {
                var existing = await _sensorRepository.GetAllAsync();
                bool isDuplicate = existing.Any(s =>
                    string.Equals(s.SensorUID, sensorCreateDto.SensorUID, StringComparison.OrdinalIgnoreCase));

                if (isDuplicate)
                    throw new InvalidOperationException(
                        $"A sensor with RFID '{sensorCreateDto.SensorUID}' already exists. Each RFID can only be registered once.");
            }

            var sensor = _mapper.Map<SENSOR>(sensorCreateDto);
            sensor.Id = Guid.NewGuid();
            sensor.CreatedOn = DateTime.UtcNow;
            sensor.IsActive = true;
            sensor.IsAssigned = false;

            await _sensorRepository.AddAsync(sensor);

            return _mapper.Map<SensorDTO>(sensor);
        }

        public async Task<bool> UpdateSensorAsync(SensorDTO sensorDto)
        {
            var sensor = await _sensorRepository.GetByIdAsync(sensorDto.Id);
            if (sensor == null) return false;

            // Selectively update only non-null fields — prevents overwriting existing data with nulls
            if (sensorDto.SensorID != null)        sensor.SensorID        = sensorDto.SensorID;
            if (sensorDto.CommonName != null)       sensor.CommonName       = sensorDto.CommonName;
            if (sensorDto.SensorUID != null)        sensor.SensorUID        = sensorDto.SensorUID;
            if (sensorDto.Type != null)             sensor.Type             = sensorDto.Type;
            if (sensorDto.Status != null)           sensor.Status           = sensorDto.Status;
            if (sensorDto.AddedBy != null)          sensor.AddedBy          = sensorDto.AddedBy;
            if (sensorDto.SENSORTYPEID.HasValue)    sensor.SENSORTYPEID     = sensorDto.SENSORTYPEID;
            if (sensorDto.Installation_date.HasValue) sensor.Installation_date = sensorDto.Installation_date;
            if (sensorDto.Expiry_date.HasValue)     sensor.Expiry_date      = sensorDto.Expiry_date;
            if (sensorDto.batteryPercentage != null) sensor.batteryPercentage = sensorDto.batteryPercentage;
            if (sensorDto.IsActive.HasValue)        sensor.IsActive         = sensorDto.IsActive;
            if (sensorDto.UpdatedBy != null)        sensor.UpdatedBy        = sensorDto.UpdatedBy;
            if (sensorDto.Accession_Number != null) sensor.Accession_Number = sensorDto.Accession_Number;
            sensor.UpdatedOn = DateTime.UtcNow;

            await _sensorRepository.UpdateAsync(sensor);
            return true;
        }

        public async Task<bool> DeleteSensorAsync(Guid sensorId)
        {
            var sensor = await _sensorRepository.GetByIdAsync(sensorId);
            if (sensor == null) return false;

            // Unlink all Trees that reference this sensor (nullify FK to avoid constraint violation)
            var allTrees = await _treeRepository.GetAllAsync();
            var linkedTrees = allTrees.Where(t => t.SENSORID == sensorId).ToList();
            foreach (var tree in linkedTrees)
            {
                tree.SENSORID   = null;
                tree.SensorType = null;
                await _treeRepository.UpdateAsync(tree);
            }

            await _sensorRepository.DeleteAsync(sensor);
            return true;
        }

        //TODO:: JP Need to move this to helper with proper Hashing
        private string HashPassword(string password)
        {
            // Replace with a secure hash in production!
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

}



