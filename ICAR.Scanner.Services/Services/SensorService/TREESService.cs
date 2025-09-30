using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.DataAccess.Repository;
using ICAR.Scanner.Models.DTOs;
using AutoMapper;

namespace ICAR.Scanner.Services.Services.TreeService;

public class TREESService : ITREESService
    {
        private readonly IRepository<Tree> _treeRepository;
        private readonly IMapper _mapper;

        public TREESService(IRepository<Tree> treeRepository, IMapper mapper)
        {
            _treeRepository = treeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Tree>> GetAllTreeAsync()
        {
            var trees = await _treeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Tree>>(trees);
        }

        public async Task<TreesDto?> GetTreeByIdAsync(Guid TreeId)
        {
            var tree = await _treeRepository.GetByIdAsync(TreeId);
            return tree == null ? null : _mapper.Map<TreesDto>(tree);
        }

        public async Task<TreesDto> CreateTreeAsync(TREESCreateDTO treeCreateDto)
        {
            var user = _mapper.Map<Tree>(treeCreateDto);
            user.Id = Guid.NewGuid();
            //user.PasswordHash = HashPassword(treeCreateDto.Password);
            user.CreatedOn = DateTime.UtcNow;
            user.IsActive = true;

            await _treeRepository.AddAsync(user);

            return _mapper.Map<TreesDto>(user);
        }

        public async Task<bool> UpdateTreeAsync(TreesDto treeDto)
        {
            var tree = await _treeRepository.GetByIdAsync(treeDto.Id);
            if (tree == null) return false;

            _mapper.Map(treeDto, tree); // Map updated fields from DTO to entity
            tree.UpdatedOn = DateTime.UtcNow;

            await _treeRepository.UpdateAsync(tree);
            return true;
        }

        public async Task<bool> DeleteTreeAsync(Guid TreeId)
        {
            var tree = await _treeRepository.GetByIdAsync(TreeId);
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



