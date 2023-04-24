using AutoMapper;
using HashidsNet;
using livro_receitas.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Mock.Test.MapperMock;

public class MapperBuilder
{
    public static IMapper Instancia()
    {
        //var configuration = new MapperConfiguration(cfg =>
        //{
        //    cfg.AddProfile<AutoMapperConfiguration>();
        //});

        //return configuration.CreateMapper();

        var builder = WebApplication.CreateBuilder();

        var a = builder.Services.AddScoped(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperConfiguration(provider.GetService<IHashids>()));
        }).CreateMapper());

        return (IMapper)a;
    }
}