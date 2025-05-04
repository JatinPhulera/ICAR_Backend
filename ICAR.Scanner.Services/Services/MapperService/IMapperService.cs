namespace ICAR.Scanner.Services.Services.MapperService;

public interface IMapperService
    {
        TDestination Map<TSource, TDestination>(TSource source);
        void Map<TSource, TDestination>(TSource source, TDestination destination);
    }