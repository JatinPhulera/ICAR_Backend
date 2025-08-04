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
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user == null ? null : _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> CreateUserAsync(UserCreateDTO userCreateDto)
        {
            var user = _mapper.Map<User>(userCreateDto);
            user.UserId = Guid.NewGuid();
            user.PasswordHash = HashPassword(userCreateDto.Password);
            user.CreatedOn = DateTime.UtcNow;
            user.IsActive = true;

            await _userRepository.AddAsync(user);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> UpdateUserAsync(UserDTO userDto)
        {
            var user = await _userRepository.GetByIdAsync(userDto.UserId);
            if (user == null) return false;

            _mapper.Map(userDto, user); // Map updated fields from DTO to entity
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



