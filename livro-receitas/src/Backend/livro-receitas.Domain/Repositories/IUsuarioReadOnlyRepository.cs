namespace livro_receitas.Domain.Repositories;

public interface IUsuarioReadOnlyRepository
{
    Task<bool> ExisteUsuario(string email);
}