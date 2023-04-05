using livro_receitas.Comunicacao.Request;

namespace livro_receitas.Application.UseCases.Receita.Registrar;

public interface IRegistrarReceitaUseCase
{
    Task Executar(RequestRegistarReceitaJson request);
}