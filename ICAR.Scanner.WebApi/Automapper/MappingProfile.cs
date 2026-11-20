using AutoMapper;
using ICAR.Scanner.Models.DTOs;
using ICAR.Scanner.DataAccess.Models;
using ICAR.Scanner.Models.DTOs.Request;
using System.Linq;

namespace ICAR.Scanner.WebApi.Automapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tree, TreesDto>();
        // TreesDto -> Tree is used for partial updates (UpdateTreeAsync maps onto an
        // existing tracked entity). Plain null can mean either "field omitted" or
        // "field explicitly cleared" — indistinguishable once deserialized — so when the
        // caller supplies the raw JSON's key set (via ProvidedKeys in Items), only map
        // fields that were actually present in the request; a field that's just a C#
        // default because the client never sent it is left untouched. Falls back to a
        // plain null-check when no key set is supplied.
        //
        // ResolutionContext has no DestinationMember in this AutoMapper version, so
        // ForAllMembers can't tell which property it's evaluating — register the
        // condition per-property instead, closing over the property name.
        var treeUpdateMap = CreateMap<TreesDto, Tree>();
        var treesDtoPropertyNames = new HashSet<string>(typeof(TreesDto).GetProperties().Select(p => p.Name));
        foreach (var property in typeof(Tree).GetProperties())
        {
            if (!property.CanWrite || !treesDtoPropertyNames.Contains(property.Name)) continue;
            var propertyName = property.Name;
            treeUpdateMap.ForMember(propertyName, opt => opt.Condition((src, dest, srcMember, destMember, ctx) =>
            {
                if (ctx.Items.TryGetValue("ProvidedKeys", out var pk) && pk is IReadOnlySet<string> providedKeys)
                {
                    return providedKeys.Contains(propertyName);
                }
                return srcMember != null;
            }));
        }

        CreateMap<TREESCreateDTO, Tree>()
            .ForMember(dest => dest.SENSORID, opt => opt.MapFrom(src => src.SensorId));

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
