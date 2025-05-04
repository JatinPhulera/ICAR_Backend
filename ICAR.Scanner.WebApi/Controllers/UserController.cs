using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.Services.Services.UserService;

namespace ICAR.Scanner.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users); // users should be List<UserDTO>
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserCreateDTO userCreateDto)
        {
            var createdUser = await _userService.CreateUserAsync(userCreateDto);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserDTO userDto)
        {
            if (id != userDto.UserId) return BadRequest();
            var result = await _userService.UpdateUserAsync(userDto);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
