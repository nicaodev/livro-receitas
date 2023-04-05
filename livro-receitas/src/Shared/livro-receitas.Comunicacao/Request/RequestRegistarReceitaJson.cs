using livro_receitas.Comunicacao.Enum;

namespace livro_receitas.Comunicacao.Request;

public class RequestRegistarReceitaJson
{

    public RequestRegistarReceitaJson()
    {
        Ingredientes = new();
    }
    public string Titulo { get; set; }
    public Categoria Categoria { get; set; }
    public string ModoPreparo { get; set; }

    public List<RequestRegistrarIngredienteJson> Ingredientes { get; set; }
}