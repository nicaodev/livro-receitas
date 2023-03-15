
using livro_receitas.Exceptions;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using Xunit;

namespace WebApi.Test.Usuario;

public class ControllerBase : IClassFixture<LivroReceitaWebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public ControllerBase(LivroReceitaWebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
        ResourceMensagensDeErro.Culture = new CultureInfo("pt-BR");
        //ResourceMensagensDeErro.Culture = CultureInfo.CurrentCulture;
    }

    protected async Task<HttpResponseMessage> PostRequest(string metodo, object body)
    {
        var jsonString = JsonConvert.SerializeObject(body);

        return await _httpClient.PostAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }

}