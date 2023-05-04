using livro_receitas.Application.Services.Criptografia;
using livro_receitas.Application.Services.Token;
using livro_receitas.Application.Services.UsuarioLogado;
using livro_receitas.Application.UseCases.Conexao.GerarQrCode;
using livro_receitas.Application.UseCases.Conexao.RecusarConexao;
using livro_receitas.Application.UseCases.Dashboard;
using livro_receitas.Application.UseCases.Login.FazerLogin;
using livro_receitas.Application.UseCases.Receita.Atualizar;
using livro_receitas.Application.UseCases.Receita.Deletar;
using livro_receitas.Application.UseCases.Receita.RecuperarPorId;
using livro_receitas.Application.UseCases.Receita.Registrar;
using livro_receitas.Application.UseCases.Usuario.AlterarSenha;
using livro_receitas.Application.UseCases.Usuario.RecuperarPerfil;
using livro_receitas.Application.UseCases.Usuario.Registrar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace livro_receitas.Application;

public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddChaveAdicionalSenha(services, configuration);
        AddHashIds(services, configuration);
        AddTokenJWT(services, configuration);
        AddUseCases(services);
        AddUserLogado(services);
    }

    private static void AddUserLogado(IServiceCollection services)
    {
        services.AddScoped<IUsuarioLogado, UsuarioLogado>();
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
    private static void AddHashIds(IServiceCollection services, IConfiguration configuration)
    {
        var salt = configuration.GetRequiredSection("Configuracoes:HashIds");
        services.AddHashids(setup =>
        {
            setup.Salt = salt.Value;
            setup.MinHashLength = 3;
        });
    }
    public static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegistrarUsuarioUserCase, RegistrarUsuarioUserCase>()
        .AddScoped<ILoginUseCase, LoginUseCase>()
        .AddScoped<IAlterarSenhaUseCase, AlterarSenhaUseCase>()
        .AddScoped<IRegistrarReceitaUseCase, RegistrarReceitaUseCase>()
        .AddScoped<IDashboard, Dashboard>()
        .AddScoped<IRecuperarReceitaPorIdUseCase, RecuperarReceitaPorIdUseCase>()
        .AddScoped<IAtualizarReceitaUseCase, AtualizarReceitaUseCase>()
        .AddScoped<IDeletarReceitaUseCase, DeletarReceitaUseCase>()
        .AddScoped<IRecuperarPerfilUseCase, RecuperarPerfilUseCase>()
        .AddScoped<IGerarQrCodeUseCase, GerarQrCodeUseCase>()
        .AddScoped<IRecusarConexaoUseCase, RecusarConexaoUseCase>();
    }
}