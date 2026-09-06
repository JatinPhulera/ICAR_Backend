using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.DataAccess.Repository;
using ICAR.Scanner.Models.DTOs;
using AutoMapper;

namespace ICAR.Scanner.Services.Services.UserService;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IRepository<User> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDTO>>(users.Where(u => u.IsActive != false).OrderByDescending(u => u.CreatedOn));
    }

    public async Task<UserDTO?> GetUserByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        return user == null ? null : _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> CreateUserAsync(UserCreateDTO userCreateDto)
    {
        var user = _mapper.Map<User>(userCreateDto);
        user.Id = Guid.NewGuid();
        user.PasswordHash = userCreateDto.Password; // TODO: hash before storing
        user.CreatedOn = DateTime.UtcNow;
        if (string.IsNullOrWhiteSpace(user.Username))
            user.Username = string.Concat(userCreateDto.FirstName, userCreateDto.LastName);
        await _userRepository.AddAsync(user);

        return _mapper.Map<UserDTO>(user);
    }

    public async Task<bool> UpdateUserAsync(UserDTO userDto)
    {
        var user = await _userRepository.GetByIdAsync(userDto.Id);
        if (user == null) return false;

        if (userDto.FirstName != null)         user.FirstName          = userDto.FirstName;
        if (userDto.LastName != null)          user.LastName           = userDto.LastName;
        if (userDto.Email != null)             user.Email              = userDto.Email;
        if (userDto.PhoneNumber != null)       user.PhoneNumber        = userDto.PhoneNumber;
        if (userDto.Address != null)           user.Address            = userDto.Address;
        if (userDto.RoleId.HasValue)           user.RoleID             = userDto.RoleId;
        if (userDto.IsActive.HasValue)         user.IsActive           = userDto.IsActive;
        if (userDto.Username != null)          user.Username           = userDto.Username;
        if (userDto.ProfilePictureUrl != null) user.ProfilePictureUrl  = userDto.ProfilePictureUrl;
        if (userDto.Latitude != null)          user.Latitude           = userDto.Latitude;
        if (userDto.Longitude != null)         user.Longitude          = userDto.Longitude;
        if (userDto.State != null)             user.State              = userDto.State;
        if (!string.IsNullOrWhiteSpace(userDto.Password))
            user.PasswordHash = userDto.Password; // TODO: hash before storing
        user.UpdatedOn = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return false;

        await _userRepository.DeleteAsync(user);
        return true;
    }

    //TODO:: JP Need to move this to helper with proper Hashing
    private string HashPassword(string password)
    {
        // Replace with a secure hash in production!
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
    }
}



