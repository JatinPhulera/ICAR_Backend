using ICAR.Scanner.Models.DTOs;

namespace ICAR.Scanner.Services.Services.InstitutionsService;

    public interface IInstitutionsService
{
        Task<IEnumerable<InstitutionsDTO>> GetAllInstitutionssAsync();
        Task<InstitutionsDTO?> GetInstitutionsByIdAsync(Guid InstitutionsId);
        Task<InstitutionsDTO> CreateInstitutionsAsync(InstitutionsCreateDTO InstitutionsCreateDto);
        Task<bool> UpdateInstitutionsAsync(InstitutionsDTO InstitutionsDto);
        Task<bool> DeleteInstitutionsAsync(Guid InstitutionsId);
    }

