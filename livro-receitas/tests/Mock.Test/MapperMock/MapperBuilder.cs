using AutoMapper;
using livro_receitas.Application.Services;

namespace Mock.Test.MapperMock;

public class MapperBuilder
{
    public static IMapper Instancia()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperConfiguration>();
        });

        return configuration.CreateMapper();
    }
}