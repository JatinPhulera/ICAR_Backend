using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.Services.Services.TreeService;
using Microsoft.AspNetCore.Mvc;

namespace ICAR.Scanner.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TREESController : ControllerBase
    {
        private readonly ITREESService _treeService;
        public TREESController(ITREESService treeService) // Add constructor to inject the dependency
        {
            _treeService = treeService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreesDto>>> GetAllTrees()
        {
            var sensors = await _treeService.GetAllTreeAsync();
            return Ok(sensors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TreesDto>> GetTree(Guid id)
        {
            var tree = await _treeService.GetTreeByIdAsync(id);
            return tree != null ? Ok(tree) : NotFound();
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
