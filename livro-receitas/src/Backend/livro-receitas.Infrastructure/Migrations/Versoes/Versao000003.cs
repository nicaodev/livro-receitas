using FluentMigrator;

namespace livro_receitas.Infrastructure.Migrations.Versoes;
[Migration((int)EnumVersoes.AlterarTabelaReceitas, "Add coluna TempoPreparo")]
public class Versao000003 : Migration
{
    public override void Down()
    {

    }

    public override void Up()
    {
        Alter.Table("receitas").AddColumn("TempoPreparo").AsInt32().NotNullable().WithDefaultValue(0);
    }
}