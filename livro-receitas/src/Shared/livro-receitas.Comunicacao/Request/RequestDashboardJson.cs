using livro_receitas.Comunicacao.Enum;

namespace livro_receitas.Comunicacao.Request;

public class RequestDashboardJson
{
    public string TituloOuIngrediente { get; set; }
    public Categoria? Categoria { get; set; }
}