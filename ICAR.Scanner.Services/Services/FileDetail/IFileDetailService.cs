using ICAR.Scanner.Models.DTOs;

namespace ICAR.Scanner.Services.Services.FileDetailService;

    public interface IFileDetailService
    {
        Task<IEnumerable<FileDetailDTO>> GetAllFileDetailsAsync();
        Task<FileDetailDTO?> GetFileDetailByIdAsync(Guid FileDetailId);
        Task<FileDetailDTO> CreateFileDetailAsync(FileDetailCreateDTO FileDetailCreateDto);
        Task<bool> UpdateFileDetailAsync(FileDetailDTO FileDetailDto);
        Task<bool> DeleteFileDetailAsync(Guid FileDetailId);
    }

