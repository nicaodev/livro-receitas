using livro_receitas.Api.Filter;
using livro_receitas.Application.UseCases.Dashboard;
using livro_receitas.Application.UseCases.Usuario.AlterarSenha;
using livro_receitas.Application.UseCases.Usuario.Registrar;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Comunicacao.Response;
using Microsoft.AspNetCore.Mvc;

namespace livro_receitas.Api.Controllers;

public class DashboardController : LivroDeReceitasController
{
    [HttpPut] // Esta como put pq precisaremos ter o corpo da requisicao.
    [ProducesResponseType(typeof(ResponseDashboardJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ServiceFilter(typeof(UsuarioAutenticadoAtribute))] //Somente este endpoint precisará estar autenticado.
    public async Task<IActionResult> RecuperarDashboard([FromServices] IDashboard useCase, [FromBody] RequestDashboardJson request)
    {

        var resultado = await useCase.Executar(request);

        if (resultado.Receitas.Any())
            return Ok(resultado);

        return NoContent();
    }
}