using FluentAssertions;
using livro_receitas.Exceptions;
using Mock.Test.Requests;
using System.Net;
using System.Text.Json;
using Xunit;

namespace WebApi.Test.Usuario.Registrar
{
    public class RegistrarUsuarioTeste : ControllerBase
    {
        private const string METODO = "usuario";
        public RegistrarUsuarioTeste(LivroReceitaWebApplicationFactory<Program> factory) : base(factory) { }

        [Fact]
        public async Task Validar_Sucesso()
        {
            var requisicao = RequestRegistrarUsuarioMock.Construir();

            var resposta = await PostRequest(METODO, requisicao);

            resposta.StatusCode.Should().Be(HttpStatusCode.Created);

            await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(respostaBody);

            responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Validar_Erro_Nome_em_branco()
        {
            var requisicao = RequestRegistrarUsuarioMock.Construir();
            requisicao.Nome = "";

            var resposta = await PostRequest(METODO, requisicao);

            resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(respostaBody);

            var erros = responseData.RootElement.GetProperty("mensagens").EnumerateArray();
            erros.Should().ContainSingle().And.Contain(c => c.GetString().Equals(ResourceMensagensDeErro.NOME_USUARIO_EM_BRANCO));
        }
    }
}