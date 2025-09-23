using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.DataAccess.Repository;
using ICAR.Scanner.Models.DTOs;
using AutoMapper;

namespace ICAR.Scanner.Services.Services.InstitutionsService;

public class InstitutionsService : IInstitutionsService
{
        private readonly IRepository<Institutions> _InstitutionsRepository;
        private readonly IMapper _mapper;

        public InstitutionsService(IRepository<Institutions> InstitutionsRepository, IMapper mapper)
        {
            _InstitutionsRepository = InstitutionsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InstitutionsDTO>> GetAllInstitutionssAsync()
        {
            var Institutionss = await _InstitutionsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<InstitutionsDTO>>(Institutionss);
        }

        public async Task<InstitutionsDTO?> GetInstitutionsByIdAsync(Guid InstitutionsId)
        {
            var Institutions = await _InstitutionsRepository.GetByIdAsync(InstitutionsId);
            return Institutions == null ? null : _mapper.Map<InstitutionsDTO>(Institutions);
        }

        public async Task<InstitutionsDTO> CreateInstitutionsAsync(InstitutionsCreateDTO InstitutionsCreateDto)
        {
            var Institutions = _mapper.Map<Institutions>(InstitutionsCreateDto);
           // Institutions.InstitutionsId = Guid.NewGuid();
            //Institutions.PasswordHash = HashPassword(InstitutionsCreateDto.Password);
            //Institutions.CreatedOn = DateTime.UtcNow;
            //Institutions.IsActive = true;

            await _InstitutionsRepository.AddAsync(Institutions);

            return _mapper.Map<InstitutionsDTO>(Institutions);
        }

        public async Task<bool> UpdateInstitutionsAsync(InstitutionsDTO InstitutionsDto)
        {
            var Institutions = await _InstitutionsRepository.GetByIdAsync(InstitutionsDto.InstitutionID);
            if (Institutions == null) return false;

            _mapper.Map(InstitutionsDto, Institutions); // Map updated fields from DTO to entity
           // Institutions.UpdatedOn = DateTime;

            await _InstitutionsRepository.UpdateAsync(Institutions);
            return true;
        }

        public async Task<bool> DeleteInstitutionsAsync(Guid InstitutionsId)
        {
            var Institutions = await _InstitutionsRepository.GetByIdAsync(InstitutionsId);
            if (Institutions == null) return false;

            await _InstitutionsRepository.DeleteAsync(Institutions);
            return true;
        }

        //TODO:: JP Need to move this to helper with proper Hashing
        private string HashPassword(string password)
        {
            // Replace with a secure hash in production!
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }



