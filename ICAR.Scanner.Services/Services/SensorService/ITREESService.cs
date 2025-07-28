using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.Models.DTOs;

namespace ICAR.Scanner.Services.Services.TreeService;

    public interface ITREESService
    {
        Task<IEnumerable<TREESDTO>> GetAllTreeAsync();
        Task<TREESDTO?> GetTreeByIdAsync(Guid treeId);
        Task<TREESDTO> CreateTreeAsync(TREESCreateDTO treeCreateDto);
        Task<bool> UpdateTreeAsync(TREESDTO treeDto);
        Task<bool> DeleteTreeAsync(Guid treeId);
    }

