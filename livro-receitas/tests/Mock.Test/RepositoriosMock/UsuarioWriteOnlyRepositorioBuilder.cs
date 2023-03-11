using livro_receitas.Application.UseCases.Usuario.Registrar;
using livro_receitas.Domain.Repositories;
using Moq;

namespace Mock.Test.RepositoriosMock;

public class UsuarioWriteOnlyRepositorioBuilder
{
    private static UsuarioWriteOnlyRepositorioBuilder _instance;
    private readonly Mock<IUsuarioWriteOnlyRepository> _repo;


    private UsuarioWriteOnlyRepositorioBuilder()
    {
        if(_repo == null)
        {
            _repo = new Mock<IUsuarioWriteOnlyRepository>();
        }
    }
    public static UsuarioWriteOnlyRepositorioBuilder Instancia()
    {
        _instance = new UsuarioWriteOnlyRepositorioBuilder();
        return _instance;
    }

    public IUsuarioWriteOnlyRepository Construir()
    {
        return _repo.Object;
    }
}