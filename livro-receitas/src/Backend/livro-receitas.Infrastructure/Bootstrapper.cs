using FluentMigrator.Runner;
using livro_receitas.Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace livro_receitas.Infrastructure;

public static class Bootstrapper
{
    public static void AddRepositorio(this IServiceCollection services, IConfiguration configuration)
    {
        AddFluentMigrator(services, configuration);
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore().ConfigureRunner(c => c.AddSqlServer().WithGlobalConnectionString(configuration.GetFullConfigConnection())
        .ScanIn(Assembly.Load("livro-receitas.Infrastructure")).For.All());
    }
}