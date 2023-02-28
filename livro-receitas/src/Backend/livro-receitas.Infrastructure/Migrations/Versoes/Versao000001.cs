using FluentMigrator;

namespace livro_receitas.Infrastructure.Migrations.Versoes;

[Migration((int)EnumVersoes.CriarTabelaUsuario, "Cria Tabela usuário")]
public class Versao000001 : Migration
{
    public override void Down()
    {
        //throw new NotImplementedException();
    }

    public override void Up()
    {
        var table = VersaoBase.InserirColunasPadrao(Create.Table("Usuario"));

        table
            .WithColumn("Nome").AsString(100).NotNullable()
            .WithColumn("Email").AsString().NotNullable()
            .WithColumn("Senha").AsString(200).NotNullable()
            .WithColumn("Telefone").AsString(14).NotNullable();
    }
}