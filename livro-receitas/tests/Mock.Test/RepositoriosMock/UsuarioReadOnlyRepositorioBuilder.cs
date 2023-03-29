using livro_receitas.Domain.Entidades;
using livro_receitas.Domain.Repositories;
using Moq;

namespace Mock.Test.RepositoriosMock;

public class UsuarioReadOnlyRepositorioBuilder
{
    private static UsuarioReadOnlyRepositorioBuilder _instance;
    private readonly Mock<IUsuarioReadOnlyRepository> _repo;

    private UsuarioReadOnlyRepositorioBuilder()
    {
        if (_repo == null)
        {
            _repo = new Mock<IUsuarioReadOnlyRepository>();
        }
    }

    public static UsuarioReadOnlyRepositorioBuilder Instancia()
    {
        _instance = new UsuarioReadOnlyRepositorioBuilder();
        return _instance;
    }

    public UsuarioReadOnlyRepositorioBuilder ExisteUsuarioComEmail(string email)
    {
        if (!string.IsNullOrEmpty(email))
            _repo.Setup(Interface => Interface.ExisteUsuario(email)).ReturnsAsync(true);

        return this; // Retorna a propria classe que esta sendo exec.
    }

    public UsuarioReadOnlyRepositorioBuilder RecuperarPorEmailSenha(Usuario usuario)
    {
        _repo.Setup(i => i.RecuperarPorEmailESenha(usuario.Email, usuario.Senha)).ReturnsAsync(usuario);
        return this;
    }

    public IUsuarioReadOnlyRepository Construir()
    {
        return _repo.Object;
    }
}