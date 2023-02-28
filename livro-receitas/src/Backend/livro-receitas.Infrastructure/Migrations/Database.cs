using Dapper;
using System.Data.SqlClient;

namespace livro_receitas.Infrastructure.Migrations;

public static class Database
{
    public static void CriarDatabase(string ConexaoBancoDeDados, string nomeDatabase)
    {
        using var minhaConexao = new SqlConnection(ConexaoBancoDeDados);

        var param = new DynamicParameters();
        param.Add("nome", nomeDatabase);

        var registros = minhaConexao.Query("SELECT * FROM SYS.DATABASES WHERE NAME= @nome", param);

        if (!registros.Any())
        {
            minhaConexao.Execute($"CREATE DATABASE {nomeDatabase}");
        }
    }
}
