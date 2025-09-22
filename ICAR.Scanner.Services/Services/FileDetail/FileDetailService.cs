using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.DataAccess.Repository;
using ICAR.Scanner.Models.DTOs;
using AutoMapper;

namespace ICAR.Scanner.Services.Services.FileDetailService;

public class FileDetailService : IFileDetailService
    {
        private readonly IRepository<FileDetail> _FileDetailRepository;
        private readonly IMapper _mapper;

        public FileDetailService(IRepository<FileDetail> FileDetailRepository, IMapper mapper)
        {
            _FileDetailRepository = FileDetailRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FileDetailDTO>> GetAllFileDetailsAsync()
        {
            var FileDetails = await _FileDetailRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FileDetailDTO>>(FileDetails);
        }

        public async Task<FileDetailDTO?> GetFileDetailByIdAsync(Guid FileDetailId)
        {
            var FileDetail = await _FileDetailRepository.GetByIdAsync(FileDetailId);
            return FileDetail == null ? null : _mapper.Map<FileDetailDTO>(FileDetail);
        }

        public async Task<FileDetailDTO> CreateFileDetailAsync(FileDetailCreateDTO FileDetailCreateDto)
        {
            var FileDetail = _mapper.Map<FileDetail>(FileDetailCreateDto);
           // FileDetail.FileDetailId = Guid.NewGuid();
            //FileDetail.PasswordHash = HashPassword(FileDetailCreateDto.Password);
            FileDetail.CreatedOn = DateTime.UtcNow;
            FileDetail.IsActive = true;

            await _FileDetailRepository.AddAsync(FileDetail);

            return _mapper.Map<FileDetailDTO>(FileDetail);
        }

        public async Task<bool> UpdateFileDetailAsync(FileDetailDTO FileDetailDto)
        {
            var FileDetail = await _FileDetailRepository.GetByIdAsync(FileDetailDto.Id);
            if (FileDetail == null) return false;

            _mapper.Map(FileDetailDto, FileDetail); // Map updated fields from DTO to entity
            FileDetail.UpdatedOn = DateTime.UtcNow;

            await _FileDetailRepository.UpdateAsync(FileDetail);
            return true;
        }

        public async Task<bool> DeleteFileDetailAsync(Guid FileDetailId)
        {
            var FileDetail = await _FileDetailRepository.GetByIdAsync(FileDetailId);
            if (FileDetail == null) return false;

            await _FileDetailRepository.DeleteAsync(FileDetail);
            return true;
        }

        //TODO:: JP Need to move this to helper with proper Hashing
        private string HashPassword(string password)
        {
            // Replace with a secure hash in production!
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }



