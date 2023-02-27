using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
