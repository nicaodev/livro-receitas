using FluentMigrator.Runner;
using livro_receitas.Domain.Extensions;
using livro_receitas.Domain.Repositories;
using livro_receitas.Domain.Repositories.Codigo;
using livro_receitas.Domain.Repositories.Receita;
using livro_receitas.Infrastructure.AcessoRepository;
using livro_receitas.Infrastructure.AcessoRepository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace livro_receitas.Infrastructure;

public static class Bootstrapper
{
    public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddFluentMigrator(services, configuration);

        AddContext(services, configuration);
        AddUnityOfWork(services);
        AddRepositorios(services);
    }

    private static void AddContext(IServiceCollection services, IConfiguration configuration)
    {
        bool.TryParse(configuration.GetSection("Configuracoes:BancoDeDadosInMemory").Value, out bool BancoDeDadosInMemory);

        if (!BancoDeDadosInMemory)
        {
            services.AddDbContext<LivroReceitasContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetFullConfigConnection(), b => b.MigrationsAssembly(typeof(LivroReceitasContext).Assembly.FullName));
            });
        }
    }

    private static void AddUnityOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnityOfWork, UnityOfWork>();
    }

    private static void AddRepositorios(IServiceCollection services)
    {
        services.AddScoped<IUsuarioReadOnlyRepository, UsuarioRepository>()
            .AddScoped<IUsuarioWriteOnlyRepository, UsuarioRepository>().
            AddScoped<IUsuarioUpdateOnlyRepository, UsuarioRepository>().
            AddScoped<IReceitaWriteOnlyRepository, ReceitaRepository>().
            AddScoped<IReceitaReadOnlyRepository, ReceitaRepository>().
            AddScoped<IUpdateOnlyRepository, ReceitaRepository>().
            AddScoped<ICodigoWriteOnlyRepository, CodigoRepository>();
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        bool.TryParse(configuration.GetSection("Configuracoes:BancoDeDadosInMemory").Value, out bool BancoDeDadosInMemory);

        if (!BancoDeDadosInMemory)
        {
            services.AddFluentMigratorCore().ConfigureRunner(c => c.AddSqlServer().WithGlobalConnectionString(configuration.GetFullConfigConnection())
        .ScanIn(Assembly.Load("livro-receitas.Infrastructure")).For.All());
        }
    }
}