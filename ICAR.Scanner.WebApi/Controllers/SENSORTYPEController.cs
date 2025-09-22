using Microsoft.AspNetCore.Mvc;
using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.Services.Services.SENSORTYPEService;
using ICAR.Scanner.DataAccess.Models;

namespace ICAR.Scanner.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SENSORTYPEController : ControllerBase
    {
        private readonly ISENSORTYPEService _SENSORTYPEervice;

        public SENSORTYPEController(ISENSORTYPEService SENSORTYPEervice)
        {
            _SENSORTYPEervice = SENSORTYPEervice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SENSORTYPEDTO>>> GetAllSENSORTYPE()
        {
            var SENSORTYPE = await _SENSORTYPEervice.GetAllSENSORTYPEsAsync();
            return Ok(SENSORTYPE); // SENSORTYPE should be List<SENSORTYPEDTO>
        }

        [HttpGet("custom")]
        public async Task<ActionResult<IEnumerable<SENSORTYPEDTO>>> GetAllSENSORTYPECustom()
        {
            var SENSORTYPE = await _SENSORTYPEervice.GetAllSENSORTYPEsAsync();

            var response = new SENSORTYPEResponse
            {
                Status = SENSORTYPE != null && SENSORTYPE.Any() ? "Success" : "NoData",
                Data = SENSORTYPE?.Select(dto => new SENSORTYPEDTO
                {
                    // Map properties from SENSORTYPEDTO to SENSORTYPE here
                    //SENSORTYPEId = dto.SENSORTYPEId,
                    SENSORTYPEID = dto.SENSORTYPEID,
                    SENSORTYPE1 = dto.SENSORTYPE1,
                    SENSORUID = dto.SENSORUID,
                    CreatedOn = dto.CreatedOn,
                    UpdatedOn = dto.UpdatedOn,
                    CreatedBy = dto.CreatedBy,
                    UpdatedBy = dto.UpdatedBy
                    //State

                    // Add other properties as needed
                }).ToList() ?? new List<SENSORTYPEDTO>()
            };

            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SENSORTYPEDTO>> GetSENSORTYPE(Guid id)
        {
            var SENSORTYPE = await _SENSORTYPEervice.GetSENSORTYPEByIdAsync(id);
            return SENSORTYPE != null ? Ok(SENSORTYPE) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<SENSORTYPEDTO>> CreateSENSORTYPE(SENSORTYPECreateDTO SENSORTYPECreateDto)
        {
            var createdSENSORTYPE = await _SENSORTYPEervice.CreateSENSORTYPEAsync(SENSORTYPECreateDto);
            return CreatedAtAction(nameof(GetSENSORTYPE), new { id = createdSENSORTYPE.SENSORTYPEID }, createdSENSORTYPE);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSENSORTYPE(Guid id, SENSORTYPEDTO SENSORTYPEDto)
        {
            if (id != SENSORTYPEDto.SENSORTYPEID) return BadRequest();
            var result = await _SENSORTYPEervice.UpdateSENSORTYPEAsync(SENSORTYPEDto);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSENSORTYPE(Guid id)
        {
            var result = await _SENSORTYPEervice.DeleteSENSORTYPEAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
