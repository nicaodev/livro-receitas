using FluentAssertions;
using livro_receitas.Application.UseCases.Usuario.Registrar;
using livro_receitas.Exceptions.ExceptionsBase;
using Mock.Test.CriptSenhaMock;
using Mock.Test.MapperMock;
using Mock.Test.RepositoriosMock;
using Mock.Test.Requests;
using Mock.Test.Token;
using System.Runtime.InteropServices;
using Xunit;

namespace UserCases.Test.Usuario.Registrar;

public class RegistrarUsuarioUseCaseTest
{
    [Fact]
    public async Task Validar_Suceso()
    {
        var request = RequestRegistrarUsuarioMock.Construir();

        var useCase = CriarUseCase();

        var resposta = await useCase.Executar(request);

        resposta.Should().NotBeNull();
        resposta.Token.Should().NotBeNullOrWhiteSpace();
    }
    [Fact]
    public async Task Validar_Erro_Email_Ja_Cadastradoo()
    {
        var request = RequestRegistrarUsuarioMock.Construir();

        var useCase = CriarUseCase(request.Email);

        Func<Task> acao = async () => { await useCase.Executar(request); };

        await acao.Should().ThrowAsync<ErroValidacaoException>().
            Where(exception => exception.MensagensDeErro.Count == 1);
    }

    private RegistrarUsuarioUserCase CriarUseCase(string email = "")
    {
        // Mockando instancias


        var mapper = MapperBuilder.Instancia();
        var repo = UsuarioWriteOnlyRepositorioBuilder.Instancia().Construir();
        var UOW = UnidadeDeTrabalhoBuilder.Instancia().Construir();
        var cript = EncriptadorSenhaBuilder.Instancia();
        var token = TokenControllerBuilder.Instancia();

        var repoUsuarioRead = UsuarioReadOnlyRepositorioBuilder.Instancia().ExisteUsuarioComEmail(email).Construir();

        return new RegistrarUsuarioUserCase(repoUsuarioRead, repo, mapper, UOW, cript, token);
    }
}