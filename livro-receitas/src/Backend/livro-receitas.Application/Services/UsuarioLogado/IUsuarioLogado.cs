using livro_receitas.Domain.Entidades;

namespace livro_receitas.Application.Services.UsuarioLogado;

public interface IUsuarioLogado
{
    Task<Usuario> RecuperarUser();
}