using AspNetCore.Hashids.Mvc;
using livro_receitas.Api.Filter;
using livro_receitas.Application.UseCases.Receita.Atualizar;
using livro_receitas.Application.UseCases.Receita.Deletar;
using livro_receitas.Application.UseCases.Receita.RecuperarPorId;
using livro_receitas.Application.UseCases.Receita.Registrar;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Comunicacao.Response;
using Microsoft.AspNetCore.Mvc;

namespace livro_receitas.Api.Controllers;

[ServiceFilter(typeof(UsuarioAutenticadoAtribute))]
public class ReceitasController : LivroDeReceitasController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseReceitaJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Registrar([FromServices] IRegistrarReceitaUseCase useCase, [FromBody] RequestRegistarReceitaJson request)
    {
        var resposta = await useCase.Executar(request);

        return Created(string.Empty, resposta);
    }

    [HttpGet]
    [Route("{id:hashids}")]
    [ProducesResponseType(typeof(ResponseReceitaJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> RecuperarPorId([FromServices] IRecuperarReceitaPorIdUseCase useCase, [FromRoute] [ModelBinder(typeof(HashidsModelBinder))] long id)
    {
        var resposta = await useCase.Executar(id);

        return Ok(resposta);
    }

    [HttpPut]
    [Route("{id:hashids}")]
    [ProducesResponseType(typeof(ResponseReceitaJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Atualizar([FromServices] IAtualizarReceitaUseCase useCase,[FromBody] RequestRegistarReceitaJson request, [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id)
    {
         await useCase.Executar(id, request);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id:hashids}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Deletar([FromServices] IDeletarReceitaUseCase useCase, [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id)
    {
        await useCase.Executar(id);

        return NoContent();
    }
}