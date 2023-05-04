using livro_receitas.Comunicacao.Request;

namespace livro_receitas.Application.UseCases.Receita.Atualizar;

public interface IAtualizarReceitaUseCase
{
    Task Executar(long id, RequestReceitaJson request);
}