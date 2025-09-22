using ICAR.Scanner.Models.DTOs;

namespace ICAR.Scanner.Services.Services.RoleMasterService;

    public interface IRoleMasterService
    {
        Task<IEnumerable<RoleMasterDTO>> GetAllRoleMastersAsync();
        Task<RoleMasterDTO?> GetRoleMasterByIdAsync(Guid RoleMasterId);
        Task<RoleMasterDTO> CreateRoleMasterAsync(RoleMasterCreateDTO RoleMasterCreateDto);
        Task<bool> UpdateRoleMasterAsync(RoleMasterDTO RoleMasterDto);
        Task<bool> DeleteRoleMasterAsync(Guid RoleMasterId);
    }

