using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.Services.Services.TreeService;
using ICAR.Scanner.Services.Services.UserService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace ICAR.Scanner.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TREESController : ControllerBase
    {
        private readonly ITREESService _treeService;
        private readonly IUserService _userService;
        public TREESController(ITREESService treeService, IUserService userService) // Add constructor to inject the dependency
        {
            _treeService = treeService;
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreesDto>>> GetAllTrees()
        {
            var sensors = await _treeService.GetAllTreeAsync();
            return Ok(sensors);
        }

        [HttpGet("custom")]
        public async Task<ActionResult<IEnumerable<TreesDto>>> GetAllTreeCustom()
        {
            var sessors = await _treeService.GetAllTreeAsync();
            var users = await _userService.GetAllUsersAsync();

            var response = new TreesResponse
            {
                Status = sessors != null && sessors.Any() ? "Success" : "NoData",
                DataCount = sessors.ToList().Count,
                Data = sessors?.Select(dto => new TreesDto
                {
                    Id = dto.Id,
                    CommonName = dto.CommonName,
                    ScientificName = dto.ScientificName,
                    AccessionNumber = dto.AccessionNumber,
                    SENSORID = dto.SENSORID,
                    Location = dto.Location,
                    LastUpdated = dto.LastUpdated,
                    Status = dto.Status,
                    CultiverName = dto.CultiverName,
                    DonorOrganization = dto.DonorOrganization,
                    Importance = dto.Importance,
                    PlaceOfOrigin = dto.PlaceOfOrigin,
                    Age = dto.Age,
                    Latitude = dto.Latitude,
                    Longitude = dto.Longitude,
                    PlantationYear = dto.PlantationYear,
                    SensorType = dto.SensorType,
                    OperatorId = dto.OperatorId,
                    OperatorFirstName = !string.IsNullOrEmpty(dto.OperatorId) && Guid.TryParse(dto.OperatorId, out var opId) ? users.FirstOrDefault(u => u.Id == opId) is { } user ? $"{user.FirstName}".Trim() : null : null,
                    OperatorLastName = !string.IsNullOrEmpty(dto.OperatorId) && Guid.TryParse(dto.OperatorId, out var opIdname) ? users.FirstOrDefault(u => u.Id == opIdname) is { } userfname ? $"{userfname.LastName}".Trim() : null : null,
                    OperatorPhone = dto.OperatorPhone,
                    OperatorState = dto.OperatorState,
                    ImageUrl = dto.ImageUrl,
                    CreatedOn = dto.CreatedOn,
                    CreatedBy = dto.CreatedBy,
                    Alerts = "healthy",
                    UniqueImportance= dto.UniqueImportance,
                    OperatorName = !string.IsNullOrEmpty(dto.OperatorId) && Guid.TryParse(dto.OperatorId, out var opIdfullname)? users.FirstOrDefault(u => u.Id == opIdfullname) is { } userfullname ? $"{userfullname.FirstName} {userfullname.LastName}".Trim(): null : null
                    //State

                    // Add other properties as needed
                }).ToList() ?? new List<TreesDto>()
            };

            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TreesDto>> GetTree(Guid id)
        {
            var tree = await _treeService.GetTreeByIdAsync(id);
            if (tree == null)
                return NotFound();

            var users = await _userService.GetAllUsersAsync();

            // Resolve OperatorName from OperatorId (string) to User's full name
            string operatorName = null;
            string operatorFirstName = null;
            string operatorLastName = null;
            if (!string.IsNullOrEmpty(tree.OperatorId) && Guid.TryParse(tree.OperatorId, out var opId))
            {
                var user = users.FirstOrDefault(u => u.Id == opId);
                if (user != null)
                {
                    operatorName = $"{user.FirstName} {user.LastName}".Trim();
                    operatorFirstName = $"{user.FirstName}".Trim();
                    operatorLastName= $"{user.LastName}".Trim();
                }
            }

            var dto = new TreesDto
            {
                Id = tree.Id,
                DisplayId = tree.DisplayId,
                AssetType = tree.AssetType,
                Location = tree.Location,
                Alerts = tree.Alerts,
                AddedBy = tree.AddedBy,
                SENSORID = tree.SENSORID,
                AccessionNumber = tree.AccessionNumber,
                AssetId = tree.AssetId,
                SensorType = tree.SensorType,
                OperatorId = tree.OperatorId,
                AddedByName = tree.AddedByName,
                OperatorName = operatorName,
                OperatorFirstName= operatorFirstName,
                OperatorLastName= operatorLastName,
                Age = tree.Age,
                AgeUnits = tree.AgeUnits,
                Latitude = tree.Latitude,
                Longitude = tree.Longitude,
                BotanicalName = tree.BotanicalName,
                ExpiryDate = tree.ExpiryDate,
                Origin = tree.Origin,
                UniqueImportance = tree.UniqueImportance,
                Value = tree.Value,
                AccessionOrigin = tree.AccessionOrigin,
                CommonName = tree.CommonName,
                ScientificName = tree.ScientificName,
                Status = tree.Status,
                CultiverName = tree.CultiverName,
                DonorOrganization = tree.DonorOrganization,
                Importance = tree.Importance,
                PlaceOfOrigin = tree.PlaceOfOrigin,
                PlantationYear = tree.PlantationYear,
                OperatorPhone = tree.OperatorPhone,
                OperatorState = tree.OperatorState,
                ImageUrl = tree.ImageUrl,
                IsActive = tree.IsActive,
                CreatedBy = tree.CreatedBy,
                UpdatedBy = tree.UpdatedBy,
                SENSOR = tree.SENSORID
            };

            return Ok(dto);
        }


        [HttpPost]
        public async Task<ActionResult<TreesDto>> CreateTree(TREESCreateDTO treeCreateDto)
        {
            var createdTree = await _treeService.CreateTreeAsync(treeCreateDto);
            return CreatedAtAction(nameof(GetTree), new { id = createdTree.Id }, createdTree);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTree(Guid id, TreesDto treeDto)
        {
            if (id != treeDto.Id) return BadRequest();
            var result = await _treeService.UpdateTreeAsync(treeDto);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTree(Guid id)
        {
            var result = await _treeService.DeleteTreeAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
