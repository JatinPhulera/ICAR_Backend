using Microsoft.AspNetCore.Mvc;
using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.Services.Services.UserService;
using ICAR.Scanner.DataAccess.Models;
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
            var roles = await _roleMasterService.GetAllRoleMastersAsync(); // Assuming this returns IEnumerable<RoleMaster>
            var institutions = await _institutionsService.GetAllInstitutionssAsync();
            var roleLookup = roles.ToDictionary(r => r.RoleID, r => r.Name);
            var institutionLookup = institutions.ToDictionary(i => i.InstitutionID, i => i.InstitutionName);

            var response = new UserResponse
            {
                Status = users != null && users.Any() ? "Success" : "NoData",
                Data = users?.Select(dto => new UserDTO
                {
                    // Map properties from UserDTO to User here
                    Id = dto.Id,
                    RoleId = dto.RoleID,
                    InstitutionId = dto.InstitutionID,
                    PhoneNumber = dto.PhoneNumber,
                    LastName = dto.LastName,
                    FirstName = dto.FirstName,
                    PasswordHash = dto.PasswordHash,
                    Username = dto.Username,
                    Email = dto.Email,
                    Address = dto.Address,
                    IsActive = dto.IsActive,
                    //LastAccessTime = dto.LastLoginAt.Value,
                    Latitude = dto.Latitude,
                    Longitude = dto.Longitude,
                    CreatedOn = dto.CreatedOn,
                    // Add RoleName property to UserDTO if not present
                    RoleName = dto.RoleID.HasValue && roleLookup.ContainsKey(dto.RoleID.Value)
                ? roleLookup[dto.RoleID.Value]
                : null
                    ,
                    InstitutionName = dto.InstitutionID.HasValue && institutionLookup.ContainsKey(dto.InstitutionID.Value)
                ? institutionLookup[dto.InstitutionID.Value]
                : null
                    //State

                    // Add other properties as needed
                }).ToList() ?? new List<UserDTO>()
            };

            return Ok(response);

        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<DashboardDTO>> GetDashboard()
        {
            var users = await _userService.GetAllUsersAsync();
            var sensors = await _sensorService.GetAllSensorsAsync();
            var trees = await _treesService.GetAllTreeAsync();

            // Map entities to DTOs if needed (replace with AutoMapper if available)
            var userDTOs = users.Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                RoleId = u.RoleID,
                InstitutionId = u.InstitutionID,
                CreatedOn = u.CreatedOn,
                LastLoginAt = u.LastLoginAt,
                Latitude = u.Latitude,
                Longitude = u.Longitude
                // Add other properties as needed
            }).ToList();

            var sensorDTOs = sensors.Select(s => new SensorDTO
            {
                Id = s.Id,
                SensorID = s.SensorID,
                Type = s.Type,
                Installation_date = s.Installation_date,
                Status = s.Status,
                AddedBy = s.AddedBy,
                CustID = s.CustID,
                AssetID = s.AssetID,
                Accession_Number = s.Accession_Number,
                SensorUID = s.SensorUID,
                Sensitivity = s.Sensitivity,
                CommonName = s.CommonName,
                UserName = s.UserName,
                Expiry_date = s.Expiry_date,
                batteryPercentage = s.batteryPercentage,
                messageType = s.messageType,
                isHooterOn = s.isHooterOn
                // Add other properties as needed
            }).ToList();

            var treeDTOs = trees.Select(t => new TreesDto
            {
                Id = t.Id,
                CommonName = t.CommonName,
                Location = t.Location,
                PlantationYear = t.PlantationYear
            }).ToList();

            var dashboard = new DashboardDTO
            {
                UserCount = userDTOs.Count,
                SensorCount = sensorDTOs.Count,
                TreeCount = treeDTOs.Count,
                Users = userDTOs,
                Sensors = sensorDTOs,
                Trees = treeDTOs
            };

            return Ok(dashboard);
        }


        [HttpGet("login")]
        public async Task<ActionResult<UserResponse>> GetLoggedInUser(string username, string password)
        {
            var users = await _userService.GetAllUsersAsync();
            var roles = await _roleMasterService.GetAllRoleMastersAsync();
            var institutions = await _institutionsService.GetAllInstitutionssAsync();
            var roleLookup = roles.ToDictionary(r => r.RoleID, r => r.Name);
            var institutionLookup = institutions.ToDictionary(i => i.InstitutionID, i => i.InstitutionName);


            // Find the user with matching username or email
            var user = users?.FirstOrDefault(u =>
                ((u.Username != null && u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)) ||
                 (u.Email != null && u.Email.Equals(username, StringComparison.OrdinalIgnoreCase))) &&
                u.PasswordHash == password
            );

            var response = new UserResponse
            {
                Status = user != null ? "Success" : "NoData",
                Data = user != null ? new List<UserDTO>
        {
            new UserDTO
            {
                Id = user.Id,
                InstitutionId = user.InstitutionID,
                RoleId = user.RoleID,
                PhoneNumber = user.PhoneNumber,
                LastName = user.LastName,
                FirstName = user.FirstName,
                PasswordHash = user.PasswordHash,
                Username = user.Username,
                Email = user.Email,
                AddressId = user.AddressId,
                Address = user.Address,
                IsActive = user.IsActive,
                LastAccessTime = user.LastLoginAt ?? DateTime.MinValue,
                Latitude = user.Latitude,
                Longitude = user.Longitude,
                CreatedOn = user.CreatedOn,
                InstitutionName = user.InstitutionID.HasValue && institutionLookup.ContainsKey(user.InstitutionID.Value)
                    ? institutionLookup[user.InstitutionID.Value]
                    : null,
                // Set RoleName using lookup
                RoleName = user.RoleID.HasValue && roleLookup.ContainsKey(user.RoleID.Value)
                    ? roleLookup[user.RoleID.Value]
                    : null
            }
        } : new List<UserDTO>()
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
