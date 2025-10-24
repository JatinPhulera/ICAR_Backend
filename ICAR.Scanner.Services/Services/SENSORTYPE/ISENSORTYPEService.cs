using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.Models.DTOs;

namespace ICAR.Scanner.Services.Services.SENSORTYPEService;

    public interface ISENSORTYPEService
{
        Task<IEnumerable<SENSORTYPE>> GetAllSENSORTYPEsAsync();
        Task<SENSORTYPEDTO?> GetSENSORTYPEByIdAsync(Guid SENSORTYPEId);
        Task<SENSORTYPEDTO> CreateSENSORTYPEAsync(SENSORTYPECreateDTO SENSORTYPECreateDto);
        Task<bool> UpdateSENSORTYPEAsync(SENSORTYPEDTO SENSORTYPEDto);
        Task<bool> DeleteSENSORTYPEAsync(Guid SENSORTYPEId);
    }

