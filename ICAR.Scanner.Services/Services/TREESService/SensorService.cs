using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.DataAccess.Repository;
using ICAR.Scanner.Models.DTOs;
using AutoMapper;
using ICAR.Scanner.Models.DTOs.Request;

namespace ICAR.Scanner.Services.Services.SensorService;

public class SensorService : ISensorService
{
        private readonly IRepository<SENSOR> _sensorRepository;
        private readonly IMapper _mapper;

        public SensorService(IRepository<SENSOR> sensorRepository, IMapper mapper)
        {
            _sensorRepository = sensorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SensorDTO>> GetAllSensorsAsync()
        {
            var users = await _sensorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SensorDTO>>(users);
        }

        public async Task<SensorDTO?> GetSensorByIdAsync(Guid userId)
        {
            var sensor = await _sensorRepository.GetByIdAsync(userId);
            return sensor == null ? null : _mapper.Map<SensorDTO>(sensor);
        }

        public async Task<SensorDTO> CreateSensorAsync(SensorCreateDTO sensorCreateDto)
        {
            var sensor = _mapper.Map<SENSOR>(sensorCreateDto);
            sensor.SENSORID = Guid.NewGuid();
            //sensor.PasswordHash = HashPassword(sensorCreateDto.Password);
            sensor.CreatedOn = DateTime.UtcNow;
            sensor.IsActive = true;

            await _sensorRepository.AddAsync(sensor);

            return _mapper.Map<SensorDTO>(sensor);
        }

        public async Task<bool> UpdateSensorAsync(SensorDTO sensorDto)
        {
            var sesnsor = await _sensorRepository.GetByIdAsync(sensorDto.SensorId);
            if (sesnsor == null) return false;

            _mapper.Map(sensorDto, sesnsor); // Map updated fields from DTO to entity
            sesnsor.UpdatedOn = DateTime.UtcNow;

            await _sensorRepository.UpdateAsync(sesnsor);
            return true;
        }

        public async Task<bool> DeleteSensorAsync(Guid userId)
        {
            var user = await _sensorRepository.GetByIdAsync(userId);
            if (user == null) return false;

            await _sensorRepository.DeleteAsync(user);
            return true;
        }

        //TODO:: JP Need to move this to helper with proper Hashing
        private string HashPassword(string password)
        {
            // Replace with a secure hash in production!
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

}



