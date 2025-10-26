using AutoMapper;
using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.Models.DTOs.Request;

namespace ICAR.Scanner.WebApi.Automapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tree, TreesDto>().ReverseMap();
        CreateMap<TREESCreateDTO, Tree>();

        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<UserCreateDTO, User>();

        CreateMap<SENSOR, SensorDTO>().ReverseMap();
        CreateMap<SensorCreateDTO, SENSOR>();

        CreateMap<AuditTree, AuditTreeDTO>().ReverseMap();
        CreateMap<AuditTreeCreateDTO, AuditTree>();

        CreateMap<SENSORTYPE, SENSORTYPEDTO>().ReverseMap();
        CreateMap<SENSORTYPECreateDTO, SENSORTYPE>();

        CreateMap<FileDetail, FileDetailDTO>().ReverseMap();
        CreateMap<FileDetailCreateDTO, FileDetail>();

        CreateMap<Institution, InstitutionsDTO>().ReverseMap();
        CreateMap<InstitutionsCreateDTO, Institution>();

        CreateMap<RoleMaster, RoleMasterDTO>().ReverseMap();
        CreateMap<RoleMasterCreateDTO, RoleMaster>();
    }
}
