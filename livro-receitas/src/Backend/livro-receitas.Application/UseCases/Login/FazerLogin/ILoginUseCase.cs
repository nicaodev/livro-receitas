using livro_receitas.Comunicacao.Request;
using livro_receitas.Comunicacao.Response;

namespace livro_receitas.Application.UseCases.Login.FazerLogin;

public interface ILoginUseCase
{
    Task<ResponseLoginJson> Executar(RequestLoginJson request);
}