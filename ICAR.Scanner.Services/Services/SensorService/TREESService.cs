using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.DataAccess.Repository;
using ICAR.Scanner.Models.DTOs;
using AutoMapper;

namespace ICAR.Scanner.Services.Services.TreeService;

public class TREESService : ITREESService
    {
        private readonly IRepository<TREE> _treeRepository;
        private readonly IMapper _mapper;

        public TREESService(IRepository<TREE> treeRepository, IMapper mapper)
        {
            _treeRepository = treeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TREESDTO>> GetAllTreeAsync()
        {
            var trees = await _treeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TREESDTO>>(trees);
        }

        public async Task<TREESDTO?> GetTreeByIdAsync(Guid treeId)
        {
            var tree = await _treeRepository.GetByIdAsync(treeId);
            return tree == null ? null : _mapper.Map<TREESDTO>(tree);
        }

        public async Task<TREESDTO> CreateTreeAsync(TREESCreateDTO treeCreateDto)
        {
            var user = _mapper.Map<TREE>(treeCreateDto);
            user.TREEID = Guid.NewGuid();
            //user.PasswordHash = HashPassword(treeCreateDto.Password);
            user.CreatedOn = DateTime.UtcNow;
            user.IsActive = true;

            await _treeRepository.AddAsync(user);

            return _mapper.Map<TREESDTO>(user);
        }

        public async Task<bool> UpdateTreeAsync(TREESDTO treeDto)
        {
            var tree = await _treeRepository.GetByIdAsync(treeDto.TREEID);
            if (tree == null) return false;

            _mapper.Map(treeDto, tree); // Map updated fields from DTO to entity
            tree.UpdatedOn = DateTime.UtcNow;

            await _treeRepository.UpdateAsync(tree);
            return true;
        }

        public async Task<bool> DeleteTreeAsync(Guid treeId)
        {
            var tree = await _treeRepository.GetByIdAsync(treeId);
            if (tree == null) return false;

            await _treeRepository.DeleteAsync(tree);
            return true;
        }

        //TODO:: JP Need to move this to helper with proper Hashing
        private string HashPassword(string password)
        {
            // Replace with a secure hash in production!
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }



