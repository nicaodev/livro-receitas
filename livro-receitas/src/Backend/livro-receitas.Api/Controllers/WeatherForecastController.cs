using livro_receitas.Application.UseCases.Usuario.Registrar;
using Microsoft.AspNetCore.Mvc;

namespace livro_receitas.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet(Name = "WeatherForecast")]
    public async Task<IActionResult> Get([FromServices] IRegistrarUsuarioUserCase useCase)
    {
        var resposta = await useCase.Executar(new Comunicacao.Request.RequestRegistrarUsuarioJson
        {
            Email = "teste@gmail.com",
            Nome = "Nicolas",
            Senha = "minhaSenha",
            Telefone = "61 9 9328-4511"
        });

        return Ok(resposta);
    }
}