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
            var sessors = await _sensorService.GetAllSensorsAsync();

            var response = new SensorResponse
            {
                Status = sessors != null && sessors.Any() ? "Success" : "NoData",
                Data = sessors?.Select(dto => new SensorDTO
                {
                    // Map properties from UserDTO to User here
                    Id = dto.Id,
                    Status = dto.Status,
                    SensorID = dto.SensorID,
                    Type = dto.Type,
                    CommonName = dto.CommonName,
                    Accession_Number = dto.Accession_Number,
                    batteryPercentage = dto.batteryPercentage,
                    Installation_date = dto.Installation_date,
                    CreatedOn = dto.CreatedOn,
                    CreatedBy = dto.CreatedBy
                    //State

                    // Add other properties as needed
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

        [HttpGet("{type}")]
        public async Task<ActionResult<SensorDTO>> GetSensorBySensorType(str id)
        {
            var sensor = await _sensorService.GetSensorByIdAsync(id);
            return sensor != null ? Ok(sensor) : NotFound();
        }
        
        [HttpPost]
        public async Task<ActionResult<SensorDTO>> CreateSensor(SensorCreateDTO sensorCreateDto)
        {
            var createdSensor = await _sensorService.CreateSensorAsync(sensorCreateDto);
            return CreatedAtAction(nameof(GetSensor), new { id = createdSensor.Id }, createdSensor);
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
            var result = await _sensorService.DeleteSensorAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
