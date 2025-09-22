using Microsoft.AspNetCore.Mvc;
using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.Services.Services.InstitutionsService;

namespace ICAR.Scanner.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstitutionsController : ControllerBase
    {
        private readonly IInstitutionsService _Institutionservice;

        public InstitutionsController(IInstitutionsService Institutionservice)
        {
            _Institutionservice = Institutionservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitutionsDTO>>> GetAllInstitutions()
        {
            var Institutions = await _Institutionservice.GetAllInstitutionssAsync();
            return Ok(Institutions); // Institutions should be List<InstitutionsDTO>
        }

        [HttpGet("custom")]
        public async Task<ActionResult<IEnumerable<InstitutionsDTO>>> GetAllInstitutionsCustom()
        {
            var Institutions = await _Institutionservice.GetAllInstitutionssAsync();

            var response = new InstitutionsResponse
            {
                Status = Institutions != null && Institutions.Any() ? "Success" : "NoData",
                Data = Institutions?.Select(dto => new InstitutionsDTO
                {
                    // Map properties from InstitutionsDTO to Institutions here
                    //InstitutionsId = dto.InstitutionsId,
                    InstitutionID = dto.InstitutionID,
                    InstitutionName = dto.InstitutionName,
                    InstitutionHead = dto.InstitutionHead,
                    InstitutionAdress = dto.InstitutionAdress,
                    Status = dto.Status,
                    CreatedOn = dto.CreatedOn,
                    UpdatedOn = dto.UpdatedOn,
                    CreatedBy = dto.CreatedBy,
                    UpdatedBy = dto.UpdatedBy
                    //State

                    // Add other properties as needed
                }).ToList() ?? new List<InstitutionsDTO>()
            };

            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstitutionsDTO>> GetInstitutions(Guid id)
        {
            var Institutions = await _Institutionservice.GetInstitutionsByIdAsync(id);
            return Institutions != null ? Ok(Institutions) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<InstitutionsDTO>> CreateInstitutions(InstitutionsCreateDTO InstitutionsCreateDto)
        {
            var createdInstitutions = await _Institutionservice.CreateInstitutionsAsync(InstitutionsCreateDto);
            return CreatedAtAction(nameof(GetInstitutions), new { id = createdInstitutions.InstitutionID }, createdInstitutions);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInstitutions(Guid id, InstitutionsDTO InstitutionsDto)
        {
            if (id != InstitutionsDto.InstitutionID) return BadRequest();
            var result = await _Institutionservice.UpdateInstitutionsAsync(InstitutionsDto);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitutions(Guid id)
        {
            var result = await _Institutionservice.DeleteInstitutionsAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
