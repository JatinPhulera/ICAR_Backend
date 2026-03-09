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
                    Id = dto.Id,
                    Name = dto.Name,
                    //ass = dto.AssetId,
                    TreeId = dto.TreeId,
                    AuditId = dto.AuditId,
                    AuditDate = dto.AuditDate,
                    Girth = dto.Girth,
                    Height = dto.Height,
                    Disease = dto.Disease,
                    Pest = dto.Pest,
                    PhysicalDamage = dto.PhysicalDamage,
                    Remarks = dto.Remarks,
                    AddedBy = dto.AddedBy,
                    LastUpdate = dto.LastUpdate,
                    State = dto.State,
                    Level = dto.Level,
                    V = dto.V,
                    ReviewedBy = dto.ReviewedBy,
                    ReviewedOn = dto.ReviewedOn,
                    AccessionNumber = dto.AccessionNumber,
                    Deletable = dto.Deletable,
                    Editable = dto.Editable,
                    Acceptable = dto.Acceptable

                    // Add other properties as needed
                }).ToList() ?? new List<AuditTreeDTO>()
            };

            return Ok(response);

        }

        [HttpGet("customauditbytreeid")]
        public async Task<ActionResult<IEnumerable<AuditTreeDTO>>> GetAllAuditTreeCustomAuditByTreeId(Guid treeId)
        {
            var AuditTree = await _AuditTreeervice.GetAllAuditTreesAsync();

            var filteredAuditTree = AuditTree?.Where(dto => dto.TreeId == treeId)
            .Select(dto => new AuditTreeDTO
            {
                Id = dto.Id,
                Name = dto.Name,
                TreeId = dto.TreeId,
                AuditId = dto.AuditId,
                AuditDate = dto.AuditDate,
                Girth = dto.Girth,
                Height = dto.Height,
                Disease = dto.Disease,
                Pest = dto.Pest,
                PhysicalDamage = dto.PhysicalDamage,
                Remarks = dto.Remarks,
                AddedBy = dto.AddedBy,
                LastUpdate = dto.LastUpdate,
                State = dto.State,
                Level = dto.Level,
                V = dto.V,
                ReviewedBy = dto.ReviewedBy,
                ReviewedOn = dto.ReviewedOn,
                AccessionNumber = dto.AccessionNumber,
                Deletable = dto.Deletable,
                Editable = dto.Editable,
                Acceptable = dto.Acceptable
                
            }).ToList() ?? new List<AuditTreeDTO>();

            var response = new AuditTreeResponse
            {
                Status = filteredAuditTree.Any() ? "Success" : "NoData",
                Data = filteredAuditTree
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
