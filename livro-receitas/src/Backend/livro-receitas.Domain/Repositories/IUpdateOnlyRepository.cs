using livro_receitas.Domain.Entidades;

namespace livro_receitas.Domain.Repositories;

public interface IUpdateOnlyRepository
{
    void Update(Usuario usuario);
}