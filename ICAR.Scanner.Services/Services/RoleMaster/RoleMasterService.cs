using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.DataAccess.Repository;
using ICAR.Scanner.Models.DTOs;
using AutoMapper;

namespace ICAR.Scanner.Services.Services.RoleMasterService;

public class RoleMasterService : IRoleMasterService
    {
        private readonly IRepository<RoleMaster> _RoleMasterRepository;
        private readonly IMapper _mapper;

        public RoleMasterService(IRepository<RoleMaster> RoleMasterRepository, IMapper mapper)
        {
            _RoleMasterRepository = RoleMasterRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleMasterDTO>> GetAllRoleMastersAsync()
        {
            var RoleMasters = await _RoleMasterRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleMasterDTO>>(RoleMasters);
        }

        public async Task<RoleMasterDTO?> GetRoleMasterByIdAsync(Guid RoleMasterId)
        {
            var RoleMaster = await _RoleMasterRepository.GetByIdAsync(RoleMasterId);
            return RoleMaster == null ? null : _mapper.Map<RoleMasterDTO>(RoleMaster);
        }

        public async Task<RoleMasterDTO> CreateRoleMasterAsync(RoleMasterCreateDTO RoleMasterCreateDto)
        {
            var RoleMaster = _mapper.Map<RoleMaster>(RoleMasterCreateDto);
            //RoleMaster.RoleMasterId = Guid.NewGuid();
            //RoleMaster.PasswordHash = HashPassword(RoleMasterCreateDto.Password);
            //RoleMaster.CreatedOn = DateTime.UtcNow;
            //RoleMaster.IsActive = true;

            await _RoleMasterRepository.AddAsync(RoleMaster);

            return _mapper.Map<RoleMasterDTO>(RoleMaster);
        }

        public async Task<bool> UpdateRoleMasterAsync(RoleMasterDTO RoleMasterDto)
        {
            var RoleMaster = await _RoleMasterRepository.GetByIdAsync(RoleMasterDto.RoleID);
            if (RoleMaster == null) return false;

            _mapper.Map(RoleMasterDto, RoleMaster); // Map updated fields from DTO to entity
            RoleMaster.UpdatedOn = DateTime.UtcNow;

            await _RoleMasterRepository.UpdateAsync(RoleMaster);
            return true;
        }

        public async Task<bool> DeleteRoleMasterAsync(Guid RoleMasterId)
        {
            var RoleMaster = await _RoleMasterRepository.GetByIdAsync(RoleMasterId);
            if (RoleMaster == null) return false;

            await _RoleMasterRepository.DeleteAsync(RoleMaster);
            return true;
        }

        //TODO:: JP Need to move this to helper with proper Hashing
        private string HashPassword(string password)
        {
            // Replace with a secure hash in production!
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }



