using AutoMapper;
using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.DataAccess.Models;

namespace ICAR.Scanner.WebApi.Automapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<UserCreateDTO, User>();
    }
}
