using livro_receitas.Application.UseCases.Usuario.Registrar;
using Microsoft.AspNetCore.Mvc;

namespace livro_receitas.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet(Name = "WeatherForecast")]
    public async Task<IActionResult> Get()
    {
        var useCase = new RegistrarUsuarioUserCase();

        await useCase.Executar(new Comunicacao.Request.RequestRegistrarUsuarioJson
        {
        });

        return Ok();
    }
}


