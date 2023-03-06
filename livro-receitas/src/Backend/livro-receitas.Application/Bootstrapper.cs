using livro_receitas.Application.Services.Criptografia;
using livro_receitas.Application.Services.Token;
using livro_receitas.Application.UseCases.Usuario.Registrar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace livro_receitas.Application;

public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddChaveAdicionalSenha(services, configuration);
        AddTokenJWT(services, configuration);

        services.AddScoped<IRegistrarUsuarioUserCase, RegistrarUsuarioUserCase>();
    }

    private static void AddChaveAdicionalSenha(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Configuracoes:ChaveAdicionalSenha");

        services.AddScoped(opt => new EncriptadorSenha(section.Value));
    }

    private static void AddTokenJWT(IServiceCollection services, IConfiguration configuration)
    {
        var sectionTempoVidaToken = configuration.GetRequiredSection("Configuracoes:TempoVidaToken");
        var sectionKey = configuration.GetRequiredSection("Configuracoes:ChaveTokenEmbase64");

        services.AddScoped(opt => new TokenController(int.Parse(sectionTempoVidaToken.Value), sectionKey.Value));
    }
}