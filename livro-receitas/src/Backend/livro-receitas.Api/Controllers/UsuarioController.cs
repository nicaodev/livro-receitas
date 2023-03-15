using livro_receitas.Application.UseCases.Usuario.Registrar;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Comunicacao.Response;
using Microsoft.AspNetCore.Mvc;

namespace livro_receitas.Api.Controllers;

public class UsuarioController : LivroDeReceitasController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseUsuarioRegistradoJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegistrarUsuario([FromServices] IRegistrarUsuarioUserCase useCase, [FromBody] RequestRegistrarUsuarioJson request)
    {
        var retorno = await useCase.Executar(request);

        return Created(string.Empty, retorno);
    }
}