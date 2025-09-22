using Microsoft.AspNetCore.Mvc;
using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.Services.Services.AuditService;

namespace ICAR.Scanner.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditTreeController : ControllerBase
    {
        private readonly IAuditTreeService _AuditTreeervice;

        public AuditTreeController(IAuditTreeService AuditTreeervice)
        {
            _AuditTreeervice = AuditTreeervice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditTreeDTO>>> GetAllAuditTree()
        {
            var AuditTree = await _AuditTreeervice.GetAllAuditTreesAsync();
            return Ok(AuditTree); // AuditTree should be List<AuditTreeDTO>
        }

        [HttpGet("custom")]
        public async Task<ActionResult<IEnumerable<AuditTreeDTO>>> GetAllAuditTreeCustom()
        {
            var AuditTree = await _AuditTreeervice.GetAllAuditTreesAsync();

            var response = new AuditTreeResponse
            {
                Status = AuditTree != null && AuditTree.Any() ? "Success" : "NoData",
                Data = AuditTree?.Select(dto => new AuditTreeDTO
                {
                    // Map properties from AuditTreeDTO to AuditTree here
                    //AuditTreeId = dto.AuditTreeId,
                    Id = dto.Id,
                    Name = dto.Name,
                    AuditId = dto.AuditId,
                    AuditDate = dto.AuditDate,
                    LastUpdate = dto.LastUpdate,
                    AccessionNumber = dto.AccessionNumber,
                    TreeId = dto.TreeId

                    // Add other properties as needed
                }).ToList() ?? new List<AuditTreeDTO>()
            };

            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuditTreeDTO>> GetAuditTree(Guid id)
        {
            var AuditTree = await _AuditTreeervice.GetAuditTreeByIdAsync(id);
            return AuditTree != null ? Ok(AuditTree) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<AuditTreeDTO>> CreateAuditTree(AuditTreeCreateDTO AuditTreeCreateDto)
        {
            var createdAuditTree = await _AuditTreeervice.CreateAuditTreeAsync(AuditTreeCreateDto);
            return CreatedAtAction(nameof(GetAuditTree), new { id = createdAuditTree.Id }, createdAuditTree);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuditTree(Guid id, AuditTreeDTO AuditTreeDto)
        {
            if (id != AuditTreeDto.Id) return BadRequest();
            var result = await _AuditTreeervice.UpdateAuditTreeAsync(AuditTreeDto);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuditTree(Guid id)
        {
            var result = await _AuditTreeervice.DeleteAuditTreeAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
