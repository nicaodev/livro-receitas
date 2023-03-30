using FluentMigrator;

namespace livro_receitas.Infrastructure.Migrations.Versoes;

[Migration((int)EnumVersoes.CriarTabelaReceitas, "Cria Tabela Receitas")]
public class Versao000002 : Migration
{
    public override void Down()
    {
        throw new NotImplementedException();
    }

    public override void Up()
    {
        CriarTabelaReceitas();
        CriarTabelaIngredientes();
    }

    private void CriarTabelaReceitas()
    {
        var table = VersaoBase.InserirColunasPadrao(Create.Table("Receitas"));

        table
            .WithColumn("Titulo").AsString(100).NotNullable()
            .WithColumn("Categoria").AsInt16().NotNullable()
            .WithColumn("ModoPreparo").AsString(5000).NotNullable();
    }

    private void CriarTabelaIngredientes()
    {
        var table = VersaoBase.InserirColunasPadrao(Create.Table("Ingredientes"));

        table
            .WithColumn("Produto").AsString(100).NotNullable()
            .WithColumn("Quantidade").AsString().NotNullable().
            WithColumn("ReceitaId").AsInt64().NotNullable().ForeignKey("FK_Ingrediente_Receita_Id", "Receitas", "Id");
    }
}