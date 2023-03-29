using livro_receitas.Comunicacao.Request;

namespace livro_receitas.Application.UseCases.Usuario.AlterarSenha;

public interface IAlterarSenhaUseCase
{
    Task Executar(RequestAlterarSenhaJson request);
}