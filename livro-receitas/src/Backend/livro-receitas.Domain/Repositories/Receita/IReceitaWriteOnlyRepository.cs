namespace livro_receitas.Domain.Repositories.Receita;

public interface IReceitaWriteOnlyRepository
{
    Task Registrar(Entidades.Receita receita);
    Task Deletar(long id);
}