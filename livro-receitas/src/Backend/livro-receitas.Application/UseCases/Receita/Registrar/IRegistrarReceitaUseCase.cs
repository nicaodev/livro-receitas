using livro_receitas.Comunicacao.Request;
using livro_receitas.Comunicacao.Response;

namespace livro_receitas.Application.UseCases.Receita.Registrar;

public interface IRegistrarReceitaUseCase
{
    Task<ResponseReceitaJson> Executar(RequestReceitaJson request);
}