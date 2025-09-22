using Microsoft.AspNetCore.Mvc;
using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.Services.Services.RoleMasterService;

namespace ICAR.Scanner.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleMasterController : ControllerBase
    {
        private readonly IRoleMasterService _RoleMasterervice;

        public RoleMasterController(IRoleMasterService RoleMasterervice)
        {
            _RoleMasterervice = RoleMasterervice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleMasterDTO>>> GetAllRoleMaster()
        {
            var RoleMaster = await _RoleMasterervice.GetAllRoleMastersAsync();
            return Ok(RoleMaster); // RoleMaster should be List<RoleMasterDTO>
        }

        [HttpGet("custom")]
        public async Task<ActionResult<IEnumerable<RoleMasterDTO>>> GetAllRoleMasterCustom()
        {
            var RoleMaster = await _RoleMasterervice.GetAllRoleMastersAsync();

            var response = new RoleMasterResponse
            {
                Status = RoleMaster != null && RoleMaster.Any() ? "Success" : "NoData",
                Data = RoleMaster?.Select(dto => new RoleMasterDTO
                {
                    // Map properties from RoleMasterDTO to RoleMaster here
                    //RoleMasterId = dto.RoleMasterId,
                    RoleID = dto.RoleID,
                    Name = dto.Name,
                    Status = dto.Status,
                    CreatedOn = dto.CreatedOn,
                    UpdatedOn = dto.UpdatedOn,
                    CreatedBy = dto.CreatedBy,
                    UpdatedBy = dto.UpdatedBy
                    //State

                    // Add other properties as needed
                }).ToList() ?? new List<RoleMasterDTO>()
            };

            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleMasterDTO>> GetRoleMaster(Guid id)
        {
            var RoleMaster = await _RoleMasterervice.GetRoleMasterByIdAsync(id);
            return RoleMaster != null ? Ok(RoleMaster) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<RoleMasterDTO>> CreateRoleMaster(RoleMasterCreateDTO RoleMasterCreateDto)
        {
            var createdRoleMaster = await _RoleMasterervice.CreateRoleMasterAsync(RoleMasterCreateDto);
            return CreatedAtAction(nameof(GetRoleMaster), new { id = createdRoleMaster.RoleID }, createdRoleMaster);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoleMaster(Guid id, RoleMasterDTO RoleMasterDto)
        {
            if (id != RoleMasterDto.RoleID) return BadRequest();
            var result = await _RoleMasterervice.UpdateRoleMasterAsync(RoleMasterDto);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleMaster(Guid id)
        {
            var result = await _RoleMasterervice.DeleteRoleMasterAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
