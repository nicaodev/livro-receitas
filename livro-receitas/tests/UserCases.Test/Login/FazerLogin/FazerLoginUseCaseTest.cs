using FluentAssertions;
using livro_receitas.Application.UseCases.Login.FazerLogin;
using livro_receitas.Exceptions;
using livro_receitas.Exceptions.ExceptionsBase;
using Mock.Test.CriptSenhaMock;
using Mock.Test.EntidadesMock;
using Mock.Test.RepositoriosMock;
using Mock.Test.Token;
using Xunit;

namespace UserCases.Test.Login.FazerLogin;

public class FazerLoginUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        (var user, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(user);

        var resposta = await useCase.Executar(new livro_receitas.Comunicacao.Request.RequestLoginJson
        {
            Email = user.Email,
            Senha = senha
        });

        resposta.Should().NotBeNull();
        resposta.Nome.Should().Be(user.Nome);
        resposta.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validar_Erro_Senha_Invalida()
    {
        (var user, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(user);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new livro_receitas.Comunicacao.Request.RequestLoginJson
            {
                Email = user.Email,
                Senha = "SenhaInvalida"
            });
        };
        await acao.Should().ThrowAsync<LoginInvalidoException>().Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }

    [Fact]
    public async Task Validar_Erro_Email_Invalido()
    {
        (var user, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(user);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new livro_receitas.Comunicacao.Request.RequestLoginJson
            {
                Email = "email_invalido@invalido.com",
                Senha = senha
            });
        };
        await acao.Should().ThrowAsync<LoginInvalidoException>().Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }

    [Fact]
    public async Task Validar_Erro_Email_Invalido_E_Senha_Invalida()
    {
        (var user, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(user);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new livro_receitas.Comunicacao.Request.RequestLoginJson
            {
                Email = "email_invalido@invalido.com",
                Senha = "senhaINvalida"
            });
        };
        await acao.Should().ThrowAsync<LoginInvalidoException>().Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }

    private LoginUseCase CriarUseCase(livro_receitas.Domain.Entidades.Usuario usuario)
    {
        var cript = EncriptadorSenhaBuilder.Instancia();
        var token = TokenControllerBuilder.Instancia();
        var repoUsuarioRead = UsuarioReadOnlyRepositorioBuilder.Instancia().RecuperarPorEmailSenha(usuario).Construir();

        return new LoginUseCase(repoUsuarioRead, cript, token);
    }
}