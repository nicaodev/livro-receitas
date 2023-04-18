using livro_receitas.Comunicacao.Enum;
using livro_receitas.Comunicacao.Request;

namespace livro_receitas.Comunicacao.Response;

public class ResponseReceitaJson
{
    public string Id { get; set; }
    public string Titulo { get; set; }
    public Categoria Categoria { get; set; }
    public string ModoPreparo { get; set; }

    public List<ResponseIngredientesJson> Ingredientes { get; set; }
}