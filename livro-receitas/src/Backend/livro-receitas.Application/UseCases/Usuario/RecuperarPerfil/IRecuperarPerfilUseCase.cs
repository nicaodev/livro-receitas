using livro_receitas.Comunicacao.Response;

namespace livro_receitas.Application.UseCases.Usuario.RecuperarPerfil;

public interface IRecuperarPerfilUseCase
{
    Task<ResponsePerfilUsuarioJson> Executar();

}