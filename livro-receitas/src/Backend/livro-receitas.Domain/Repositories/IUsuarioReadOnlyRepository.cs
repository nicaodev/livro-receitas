using livro_receitas.Domain.Entidades;

namespace livro_receitas.Domain.Repositories;

public interface IUsuarioReadOnlyRepository
{
    Task<bool> ExisteUsuario(string email);
    Task<Usuario> RecuperarPorEmailESenha(string email, string senha);
    Task<Usuario> RecuperarPorEmail(string email);
}