using Microsoft.Extensions.Configuration;

namespace livro_receitas.Domain.Extensions;

public static class RepositorioExtension
{
    public static string GetDefaultConnection(this IConfiguration configuration)
    {
        var DefaultConnection = configuration.GetConnectionString("DefaultConnection");

        return DefaultConnection;
    }

    public static string GetDefaultNameDatabase(this IConfiguration configuration)
    {
        var DefaultNameDatabase = configuration.GetConnectionString("DefaultNameDatabase");

        return DefaultNameDatabase;
    }

    public static string GetFullConfigConnection(this IConfiguration configuration)
    {
        var DefaultConnection = configuration.GetDefaultConnection();
        var DefaultNameDatabase = configuration.GetDefaultNameDatabase();

        return $"{DefaultConnection}Database={DefaultNameDatabase}";
    }
}