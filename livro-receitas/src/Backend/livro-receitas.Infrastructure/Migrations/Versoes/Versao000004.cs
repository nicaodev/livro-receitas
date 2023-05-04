using FluentMigrator;

namespace livro_receitas.Infrastructure.Migrations.Versoes;
[Migration((int)EnumVersoes.CriarTabelasAssociacaoUsuario, "Add tabelas para associação de usuários")]
public class Versao000004 : Migration
{
    public override void Down()
    {

    }

    public override void Up()
    {
        var table = VersaoBase.InserirColunasPadrao(Create.Table("Codigos"));

        
        table
            .WithColumn("Codigo").AsString(200).NotNullable()
            .WithColumn("UsuarioId").AsInt64().NotNullable().ForeignKey("FK_Codigo_Usuario_Id", "Usuarios", "Id");
    }
}