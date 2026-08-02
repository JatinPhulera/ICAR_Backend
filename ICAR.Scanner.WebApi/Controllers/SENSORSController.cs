using ICAR.Scanner.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using ICAR.Scanner.Services.Services.SensorService;
using ICAR.Scanner.Models.DTOs.Request;

namespace ICAR.Scanner.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SENSORSController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SENSORSController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorDTO>>> GetAllSensors()
        {
            var sensors = await _sensorService.GetAllSensorsAsync();
            return Ok(sensors);
        }

        [HttpGet("custom")]
        public async Task<ActionResult<IEnumerable<SensorDTO>>> GetAllSensorCustom()
        {
            var sessors = await _sensorService.GetUnassignedActiveSensorsAsync();

            var response = new SensorResponse
            {
                Status = sessors != null && sessors.Any() ? "Success" : "NoData",
                Data = sessors?.Select(dto => new SensorDTO
                {
                    Id = dto.Id,
                    Status = dto.Status,
                    SensorID = dto.SensorID,
                    Type = dto.Type,
                    CommonName = dto.CommonName,
                    Accession_Number = dto.Accession_Number,
                    batteryPercentage = dto.batteryPercentage,
                    Installation_date = dto.Installation_date,
                    CreatedOn = dto.CreatedOn,
                    CreatedBy = dto.CreatedBy,
                    SENSORTYPEID = dto.SENSORTYPEID,
                    SensorUID = dto.SensorUID,
                    IsActive = dto.IsActive,
                    IsAssigned = dto.IsAssigned,
                }).ToList() ?? new List<SensorDTO>()
            };

            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SensorDTO>> GetSensor(Guid id)
        {
            var sensor = await _sensorService.GetSensorByIdAsync(id);
            return sensor != null ? Ok(sensor) : NotFound();
        }
        

        [HttpPost]
        public async Task<ActionResult<SensorDTO>> CreateSensor(SensorCreateDTO sensorCreateDto)
        {
            try
            {
                var createdSensor = await _sensorService.CreateSensorAsync(sensorCreateDto);
                return CreatedAtAction(nameof(GetSensor), new { id = createdSensor.Id }, createdSensor);
            }
            catch (InvalidOperationException ex)
            {
                // Duplicate RFID — return 409 Conflict with a clear message
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSensor(Guid id, SensorDTO sensorDto)
        {
            if (id != sensorDto.Id) return BadRequest();
            var result = await _sensorService.UpdateSensorAsync(sensorDto);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(Guid id)
        {
            try
            {
                var result = await _sensorService.DeleteSensorAsync(id);
                return result ? NoContent() : NotFound(new { message = "Sensor not found." });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = $"Cannot delete sensor: {ex.InnerException?.Message ?? ex.Message}" });
            }
        }
    }
}
