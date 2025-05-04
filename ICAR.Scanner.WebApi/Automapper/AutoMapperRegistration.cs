using ICAR.Scanner.Services.Services.MapperService;
using ICAR.Scanner.WebApi.Automapper;

namespace ICAR.Scanner.WebApi.Automapper;
  
 public static class AutoMapperRegistration
    {
        public static IServiceCollection AddICARAutoMapper(this IServiceCollection services)
        {
            // Registers all profiles in the assembly
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            // Optionally, register your generic mapper service
            services.AddScoped<IMapperService, MapperService>();
            return services;
        }
    }