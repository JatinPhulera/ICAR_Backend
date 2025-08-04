using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.Services.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using ICAR.Scanner.Services.Services.SensorService;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<SensorDTO>> GetSensor(Guid id)
        {
            var sensor = await _sensorService.GetSensorByIdAsync(id);
            return sensor != null ? Ok(sensor) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<SensorDTO>> CreateSensor(SensorCreateDTO sensorCreateDto)
        {
            var createdSensor = await _sensorService.CreateSensorAsync(sensorCreateDto);
            return CreatedAtAction(nameof(GetSensor), new { id = createdSensor.SENSORID }, createdSensor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSensor(Guid id, SensorDTO sensorDto)
        {
            if (id != sensorDto.SENSORID) return BadRequest();
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
