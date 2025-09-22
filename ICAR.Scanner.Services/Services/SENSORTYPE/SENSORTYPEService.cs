using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.DataAccess.Repository;
using ICAR.Scanner.Models.DTOs;
using AutoMapper;

namespace ICAR.Scanner.Services.Services.SENSORTYPEService;

public class SENSORTYPEService : ISENSORTYPEService
    {
        private readonly IRepository<SENSORTYPE> _SENSORTYPERepository;
        private readonly IMapper _mapper;

        public SENSORTYPEService(IRepository<SENSORTYPE> SENSORTYPERepository, IMapper mapper)
        {
            _SENSORTYPERepository = SENSORTYPERepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SENSORTYPEDTO>> GetAllSENSORTYPEsAsync()
        {
            var SENSORTYPEs = await _SENSORTYPERepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SENSORTYPEDTO>>(SENSORTYPEs);
        }

        public async Task<SENSORTYPEDTO?> GetSENSORTYPEByIdAsync(Guid SENSORTYPEId)
        {
            var SENSORTYPE = await _SENSORTYPERepository.GetByIdAsync(SENSORTYPEId);
            return SENSORTYPE == null ? null : _mapper.Map<SENSORTYPEDTO>(SENSORTYPE);
        }

        public async Task<SENSORTYPEDTO> CreateSENSORTYPEAsync(SENSORTYPECreateDTO SENSORTYPECreateDto)
        {
            var SENSORTYPE = _mapper.Map<SENSORTYPE>(SENSORTYPECreateDto);
            //SENSORTYPE.SENSORTYPEId = Guid.NewGuid();
            //SENSORTYPE.PasswordHash = HashPassword(SENSORTYPECreateDto.Password);
            //SENSORTYPE.CreatedOn = DateTime.UtcNow;
            //SENSORTYPE.IsActive = true;

            await _SENSORTYPERepository.AddAsync(SENSORTYPE);

            return _mapper.Map<SENSORTYPEDTO>(SENSORTYPE);
        }

        public async Task<bool> UpdateSENSORTYPEAsync(SENSORTYPEDTO SENSORTYPEDto)
        {
            var SENSORTYPE = await _SENSORTYPERepository.GetByIdAsync(SENSORTYPEDto.SENSORTYPEID);
            if (SENSORTYPE == null) return false;

            _mapper.Map(SENSORTYPEDto, SENSORTYPE); // Map updated fields from DTO to entity
            SENSORTYPE.UpdatedOn = DateTime.UtcNow;

            await _SENSORTYPERepository.UpdateAsync(SENSORTYPE);
            return true;
        }

        public async Task<bool> DeleteSENSORTYPEAsync(Guid SENSORTYPEId)
        {
            var SENSORTYPE = await _SENSORTYPERepository.GetByIdAsync(SENSORTYPEId);
            if (SENSORTYPE == null) return false;

            await _SENSORTYPERepository.DeleteAsync(SENSORTYPE);
            return true;
        }

        //TODO:: JP Need to move this to helper with proper Hashing
        private string HashPassword(string password)
        {
            // Replace with a secure hash in production!
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }



