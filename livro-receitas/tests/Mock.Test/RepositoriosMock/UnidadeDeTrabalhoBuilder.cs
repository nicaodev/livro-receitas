using livro_receitas.Domain.Repositories;
using Moq;

namespace Mock.Test.RepositoriosMock;

public class UnidadeDeTrabalhoBuilder
{
    private static UnidadeDeTrabalhoBuilder _instance;
    private readonly Mock<IUnityOfWork> _repo;

    public UnidadeDeTrabalhoBuilder()
    {
        if (_repo == null)
            _repo = new Mock<IUnityOfWork>();
    }

    public static UnidadeDeTrabalhoBuilder Instancia()
    {
        _instance = new UnidadeDeTrabalhoBuilder();
        return _instance;
    }

    public IUnityOfWork Construir()
    {
        return _repo.Object;
    }
}