using Microsoft.AspNetCore.Mvc;
using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.Services.Services.UserService;
using ICAR.Scanner.Services.Services.RoleMasterService;
using ICAR.Scanner.Services.Services.InstitutionsService;
using ICAR.Scanner.Services.Services.SensorService;
using ICAR.Scanner.Services.Services.TreeService;

namespace ICAR.Scanner.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleMasterService _roleMasterService;
        private readonly IInstitutionsService _institutionsService;
        private readonly ISensorService _sensorService;
        private readonly ITREESService _treesService;

        public UsersController(IUserService userService, IRoleMasterService roleMasterService, IInstitutionsService institutionsService, ISensorService sensorService,
    ITREESService treesService)
        {
            _userService = userService;
            _roleMasterService = roleMasterService;
            _institutionsService = institutionsService;
            _sensorService = sensorService;
            _treesService = treesService;
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
            var roles = await _roleMasterService.GetAllRoleMastersAsync();
            var institutions = await _institutionsService.GetAllInstitutionssAsync();
            var roleLookup = roles.ToDictionary(r => r.RoleID, r => r.Name);
            var institutionLookup = institutions.ToDictionary(i => i.InstitutionID, i => i.InstitutionName);

            var userList = users.ToList();
            foreach (var dto in userList)
            {
                if (dto.RoleId.HasValue && roleLookup.TryGetValue(dto.RoleId.Value, out var roleName))
                    dto.RoleName = roleName;
                if (dto.InstitutionId.HasValue && institutionLookup.TryGetValue(dto.InstitutionId.Value, out var instName))
                    dto.InstitutionName = instName;
            }

            var response = new UserResponse
            {
                Status = userList.Any() ? "Success" : "NoData",
                Data = userList
            };

            return Ok(response);
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<DashboardDTO>> GetDashboard()
        {
            var users = await _userService.GetAllUsersAsync();
            var sensors = await _sensorService.GetAllSensorsAsync();
            var trees = await _treesService.GetAllTreeAsync();

            var userList   = users.ToList();
            var sensorList = sensors.ToList();
            var treeList   = trees.ToList();

            var dashboard = new DashboardDTO
            {
                UserCount   = userList.Count,
                SensorCount = sensorList.Count,
                TreeCount   = treeList.Count,
                Users       = userList,
                Sensors     = sensorList,
                Trees       = treeList,
            };

            return Ok(dashboard);
        }


        [HttpGet("login")]
        public async Task<ActionResult<UserResponse>> GetLoggedInUser(string username, string password)
        {
            // Dev bypass — always works for quick testing
            if (username.Equals("admin", StringComparison.OrdinalIgnoreCase) && password == "admin123")
            {
                var devUser = new UserDTO
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Username = "admin",
                    Email = "admin@icar.com",
                    FirstName = "Admin",
                    LastName = "User",
                    RoleName = "Admin",
                    IsActive = true,
                    CreatedOn = DateTime.UtcNow,
                };
                return Ok(new UserResponse { Status = "Success", Data = new List<UserDTO> { devUser } });
            }

            var users = await _userService.GetAllUsersAsync();
            var roles = await _roleMasterService.GetAllRoleMastersAsync();
            var institutions = await _institutionsService.GetAllInstitutionssAsync();
            var roleLookup = roles.ToDictionary(r => r.RoleID, r => r.Name);
            var institutionLookup = institutions.ToDictionary(i => i.InstitutionID, i => i.InstitutionName);

            var user = users?.FirstOrDefault(u =>
                ((u.Username != null && u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)) ||
                 (u.Email != null && u.Email.Equals(username, StringComparison.OrdinalIgnoreCase))) &&
                u.PasswordHash == password
            );

            if (user != null)
            {
                if (user.RoleId.HasValue && roleLookup.TryGetValue(user.RoleId.Value, out var roleName))
                    user.RoleName = roleName;
                if (user.InstitutionId.HasValue && institutionLookup.TryGetValue(user.InstitutionId.Value, out var instName))
                    user.InstitutionName = instName;
            }

            var response = new UserResponse
            {
                Status = user != null ? "Success" : "NoData",
                Data = user != null ? new List<UserDTO> { user } : new List<UserDTO>()
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
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserDTO userDto)
        {
            if (id != userDto.Id) return BadRequest();
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
