using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.Models.DTOs;

namespace ICAR.Scanner.Services.Services.UserService;

    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(Guid userId);
        Task<UserDTO> CreateUserAsync(UserCreateDTO userCreateDto);
        Task<bool> UpdateUserAsync(UserDTO userDto);
        Task<bool> DeleteUserAsync(Guid userId);
    }

