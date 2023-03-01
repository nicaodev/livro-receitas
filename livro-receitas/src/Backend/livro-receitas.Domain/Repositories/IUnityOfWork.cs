namespace livro_receitas.Domain.Repositories
{
    public interface IUnityOfWork
    {
        Task Commit();
    }
}