using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.Models.DTOs.Request;

namespace ICAR.Scanner.Services.Services.SensorService;

public interface ISensorService
{
    Task<IEnumerable<SENSOR>> GetAllSensorsAsync();
    Task<SensorDTO?> GetSensorByIdAsync(Guid userId);
    Task<SensorDTO> CreateSensorAsync(SensorCreateDTO sesorCreateDto);
    Task<bool> UpdateSensorAsync(SensorDTO userDto);
    Task<bool> DeleteSensorAsync(Guid userId);
}

