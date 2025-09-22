using Microsoft.AspNetCore.Mvc;
using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.Services.Services.FileDetailService;

namespace ICAR.Scanner.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileDetailController : ControllerBase
    {
        private readonly IFileDetailService _FileDetailervice;

        public FileDetailController(IFileDetailService FileDetailervice)
        {
            _FileDetailervice = FileDetailervice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileDetailDTO>>> GetAllFileDetail()
        {
            var FileDetail = await _FileDetailervice.GetAllFileDetailsAsync();
            return Ok(FileDetail); // FileDetail should be List<FileDetailDTO>
        }

        [HttpGet("custom")]
        public async Task<ActionResult<IEnumerable<FileDetailDTO>>> GetAllFileDetailCustom()
        {
            var FileDetail = await _FileDetailervice.GetAllFileDetailsAsync();

            var response = new FileDetailResponse
            {
                Status = FileDetail != null && FileDetail.Any() ? "Success" : "NoData",
                Data = FileDetail?.Select(dto => new FileDetailDTO
                {
                    // Map properties from FileDetailDTO to FileDetail here
                   // FileDetailId = dto.FileDetailId,
                    RoleId = dto.RoleId,
                    PhoneNumber = dto.PhoneNumber,
                    LastName = dto.LastName,
                    FirstName = dto.FirstName,
                    //FileDetailname = dto.FileDetailname,
                    Email = dto.Email,
                    AddressId = dto.AddressId
                    //State

                    // Add other properties as needed
                }).ToList() ?? new List<FileDetailDTO>()
            };

            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FileDetailDTO>> GetFileDetail(Guid id)
        {
            var FileDetail = await _FileDetailervice.GetFileDetailByIdAsync(id);
            return FileDetail != null ? Ok(FileDetail) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<FileDetailDTO>> CreateFileDetail(FileDetailCreateDTO FileDetailCreateDto)
        {
            var createdFileDetail = await _FileDetailervice.CreateFileDetailAsync(FileDetailCreateDto);
            return CreatedAtAction(nameof(GetFileDetail), new { id = createdFileDetail.UserId }, createdFileDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFileDetail(Guid id, FileDetailDTO FileDetailDto)
        {
            if (id != FileDetailDto.UserId) return BadRequest();
            var result = await _FileDetailervice.UpdateFileDetailAsync(FileDetailDto);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFileDetail(Guid id)
        {
            var result = await _FileDetailervice.DeleteFileDetailAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
