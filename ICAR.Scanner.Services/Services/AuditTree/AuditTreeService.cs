using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.DataAccess.Repository;
using ICAR.Scanner.Models.DTOs;
using AutoMapper;

namespace ICAR.Scanner.Services.Services.AuditService;

public class AuditTreeService : IAuditTreeService
    {
        private readonly IRepository<AuditTree> _AuditTreeRepository;
        private readonly IMapper _mapper;

        public AuditTreeService(IRepository<AuditTree> AuditTreeRepository, IMapper mapper)
        {
            _AuditTreeRepository = AuditTreeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuditTreeDTO>> GetAllAuditTreesAsync()
        {
            var AuditTrees = await _AuditTreeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuditTreeDTO>>(AuditTrees);
        }

        public async Task<AuditTreeDTO?> GetAuditTreeByIdAsync(Guid AuditTreeId)
        {
            var AuditTree = await _AuditTreeRepository.GetByIdAsync(AuditTreeId);
            return AuditTree == null ? null : _mapper.Map<AuditTreeDTO>(AuditTree);
        }

        public async Task<AuditTreeDTO> CreateAuditTreeAsync(AuditTreeCreateDTO AuditTreeCreateDto)
        {
            var AuditTree = _mapper.Map<AuditTree>(AuditTreeCreateDto);
            AuditTree.Id = Guid.NewGuid();
            //AuditTree.PasswordHash = HashPassword(AuditTreeCreateDto.Password);
            AuditTree.CreatedOn = AuditTree.AuditDate = DateTime.UtcNow;
            AuditTree.IsActive = true;

            await _AuditTreeRepository.AddAsync(AuditTree);

            return _mapper.Map<AuditTreeDTO>(AuditTree);
        }

        public async Task<bool> UpdateAuditTreeAsync(AuditTreeDTO AuditTreeDto)
        {
            var AuditTree = await _AuditTreeRepository.GetByIdAsync(AuditTreeDto.Id);
            if (AuditTree == null) return false;

            if (AuditTreeDto.Name != null)       AuditTree.Name       = AuditTreeDto.Name;
            if (AuditTreeDto.Girth != null)      AuditTree.Girth      = AuditTreeDto.Girth;
            if (AuditTreeDto.Height != null)     AuditTree.Height     = AuditTreeDto.Height;
            if (AuditTreeDto.Remarks != null)    AuditTree.Remarks    = AuditTreeDto.Remarks;
            if (AuditTreeDto.State != null)      AuditTree.State      = AuditTreeDto.State;
            if (AuditTreeDto.Level != null)      AuditTree.Level      = AuditTreeDto.Level;
            if (AuditTreeDto.ReviewedBy != null) AuditTree.ReviewedBy = AuditTreeDto.ReviewedBy;
            if (AuditTreeDto.AddedBy != null)    AuditTree.AddedBy    = AuditTreeDto.AddedBy;
            if (AuditTreeDto.UpdatedBy != null)  AuditTree.UpdatedBy  = AuditTreeDto.UpdatedBy;
            // Disease/Pest/PhysicalDamage are set unconditionally — null = user cleared the toggle
            AuditTree.Disease        = AuditTreeDto.Disease;
            AuditTree.Pest           = AuditTreeDto.Pest;
            AuditTree.PhysicalDamage = AuditTreeDto.PhysicalDamage;
            AuditTree.UpdatedOn = DateTime.UtcNow;

            await _AuditTreeRepository.UpdateAsync(AuditTree);
            return true;
        }

        public async Task<bool> DeleteAuditTreeAsync(Guid AuditTreeId)
        {
            var AuditTree = await _AuditTreeRepository.GetByIdAsync(AuditTreeId);
            if (AuditTree == null) return false;

            await _AuditTreeRepository.DeleteAsync(AuditTree);
            return true;
        }

        //TODO:: JP Need to move this to helper with proper Hashing
        private string HashPassword(string password)
        {
            // Replace with a secure hash in production!
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }



