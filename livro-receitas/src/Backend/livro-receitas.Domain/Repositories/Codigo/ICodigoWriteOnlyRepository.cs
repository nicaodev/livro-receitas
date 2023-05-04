using livro_receitas.Domain.Entidades;

namespace livro_receitas.Domain.Repositories.Codigo;

public interface ICodigoWriteOnlyRepository
{
    Task Registrar(Codigos codigo);
    Task Deletar(long userId);
}