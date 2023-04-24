namespace livro_receitas.Domain.Repositories.Receita;

public interface IReceitaReadOnlyRepository
{
    Task<IList<Entidades.Receita>> RecuperarTodasDoUsuario(long idUsuario);
    Task<Entidades.Receita> RecuperarPorId(long receitdaId);
}