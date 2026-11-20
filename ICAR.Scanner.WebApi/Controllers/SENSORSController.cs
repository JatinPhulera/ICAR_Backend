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
            // Full list, wrapped in the {status, data} shape — used by the admin
            // Sensor Management screen, which must show assigned/inactive sensors too.
            var sensors = await _sensorService.GetAllSensorsAsync();
            return Ok(ToSensorResponse(sensors));
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<SensorDTO>>> GetAvailableSensors()
        {
            // Unassigned + active only — used by the tree-assignment picker so
            // a sensor already in use can't be selected and rejected with a 400.
            var sensors = await _sensorService.GetUnassignedActiveSensorsAsync();
            return Ok(ToSensorResponse(sensors));
        }

        private static SensorResponse ToSensorResponse(IEnumerable<SensorDTO>? sensors)
        {
            return new SensorResponse
            {
                Status = sensors != null && sensors.Any() ? "Success" : "NoData",
                Data = sensors?.Select(dto => new SensorDTO
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
