using livro_receitas.Comunicacao.Response;

namespace livro_receitas.Application.UseCases.Receita.RecuperarPorId;

public interface IRecuperarReceitaPorIdUseCase
{
    Task<ResponseReceitaJson> Executar(long id);
}