using FluentAssertions;
using livro_receitas.Comunicacao.Request;
using System.Net;
using System.Text.Json;
using WebApi.Test.Usuario;
using Xunit;

namespace WebApi.Test.Login.FazerLogin;

public class LoginTest : ControllerBase
{
    private const string METODO = "login";

    private livro_receitas.Domain.Entidades.Usuario _usuario;
    private string _senha;

    public LoginTest(LivroReceitaWebApplicationFactory<Program> factory) : base(factory)
    {
        _usuario = factory.retornaUsuarioCriadoInMemory();
        _senha = factory.retornaSenhaUsuarioCriadoInMemory();
    }

    [Fact]
    public async Task Validar_Sucesso()
    {
        var requisicao = new RequestLoginJson
        {
            Email = _usuario.Email,
            Senha = _senha
        };

        var resposta = await PostRequest(METODO, requisicao);

        resposta.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(respostaBody);

        responseData.RootElement.GetProperty("nome").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_usuario.Nome);
        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }
}