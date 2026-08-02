using ICAR.Scanner.Models.DTOs;

namespace ICAR.Scanner.Services.Services.AuditService;

    public interface IAuditTreeService
    {
        Task<IEnumerable<AuditTreeDTO>> GetAllAuditTreesAsync();
        Task<AuditTreeDTO?> GetAuditTreeByIdAsync(Guid AuditTreeId);
        Task<AuditTreeDTO> CreateAuditTreeAsync(AuditTreeCreateDTO AuditTreeCreateDto);
        Task<bool> UpdateAuditTreeAsync(AuditTreeDTO AuditTreeDto);
        Task<bool> DeleteAuditTreeAsync(Guid AuditTreeId);
    }

