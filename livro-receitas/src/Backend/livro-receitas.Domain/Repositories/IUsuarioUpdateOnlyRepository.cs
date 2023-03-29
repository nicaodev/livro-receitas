using livro_receitas.Domain.Entidades;

namespace livro_receitas.Domain.Repositories;

public interface IUsuarioUpdateOnlyRepository
{
    void Update(Usuario usuario);

    Task<Usuario> RecuperarPorId(long id);
}