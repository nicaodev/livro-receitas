using livro_receitas.Application.UseCases.Login.FazerLogin;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Comunicacao.Response;
using Microsoft.AspNetCore.Mvc;

namespace livro_receitas.Api.Controllers
{
    public class LoginController : LivroDeReceitasController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromServices] ILoginUseCase loginUseCase, [FromBody] RequestLoginJson request)
        {
            var resposta = await loginUseCase.Executar(request);

            return Ok(resposta);
        }
    }
}