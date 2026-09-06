using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.Models.DTOs;

namespace ICAR.Scanner.Services.Services.TreeService;

    public interface ITREESService
    {
        Task<IEnumerable<TreesDto>> GetAllTreeAsync();
        Task<TreesDto?> GetTreeByIdAsync(Guid TreeId);
        Task<TreesDto?> GetTreeByRfidAsync(string rfid);
        Task<TreesDto> CreateTreeAsync(TREESCreateDTO treeCreateDto);
        Task<bool> UpdateTreeAsync(TreesDto treeDto);
        Task<bool> DeleteTreeAsync(Guid TreeId);
    }

