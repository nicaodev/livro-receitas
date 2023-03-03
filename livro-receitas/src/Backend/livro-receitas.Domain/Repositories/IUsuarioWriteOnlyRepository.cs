using livro_receitas.Domain.Entidades;

namespace livro_receitas.Domain.Repositories;

public interface IUsuarioWriteOnlyRepository
{
    Task Adicionar(Usuario usuario);
}