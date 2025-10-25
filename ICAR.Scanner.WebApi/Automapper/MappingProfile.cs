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
    }
}
