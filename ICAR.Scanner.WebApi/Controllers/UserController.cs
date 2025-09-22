using Microsoft.AspNetCore.Mvc;
using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.Services.Services.UserService;
using ICAR.Scanner.DataAccess.Models;

namespace ICAR.Scanner.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAuditTreeService _userService;

        public UsersController(IAuditTreeService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users); // users should be List<UserDTO>
        }

        [HttpGet("custom")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUserCustom()
        {
            var users = await _userService.GetAllUsersAsync();

            var response = new UserResponse
            {
                Status = users != null && users.Any() ? "Success" : "NoData",
                Data = users?.Select(dto => new UserDTO
                {
                    // Map properties from UserDTO to User here
                    UserId = dto.UserId,
                    RoleId = dto.RoleId,
                    PhoneNumber = dto.PhoneNumber,
                    LastName = dto.LastName,
                    FirstName = dto.FirstName,
                    Username = dto.Username,
                    Email = dto.Email,
                    AddressId = dto.AddressId,
                    LastAccessTime=dto.LastAccessTime,
                    Latitude=dto.Latitude,
                    Longitude = dto.Longitude
                    //State

                    // Add other properties as needed
                }).ToList() ?? new List<UserDTO>()
            };

            return Ok(response);

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
