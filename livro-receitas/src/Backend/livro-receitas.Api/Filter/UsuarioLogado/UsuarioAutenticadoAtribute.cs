using livro_receitas.Application.Services.Token;
using livro_receitas.Application.Services.UsuarioLogado;
using livro_receitas.Comunicacao.Response;
using livro_receitas.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace livro_receitas.Api.Filter.UsuarioLogado;

public class UsuarioAutenticadoAtribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepository _usuarioLogado;
    public UsuarioAutenticadoAtribute(TokenController tokenController, IUsuarioReadOnlyRepository usuarioLogado)
    {
        _tokenController = tokenController;
        _usuarioLogado = usuarioLogado;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenNaRequisicao(context);
            var email = _tokenController.RecuperarEmail(token);
            var usuario = _usuarioLogado.RecuperarPorEmail(email);

            if (usuario is null)
                throw new Exception();
        }
        catch (SecurityTokenExpiredException)
        {
            TokenExpirado(context);
        }
        catch
        {
            UsuarioSemPermissao(context);
        }
    }

    private static string TokenNaRequisicao(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authorization))
            throw new Exception();


        return authorization["Bearer".Length..].Trim();
    }

    private static void TokenExpirado(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ResponseError("Token expirado."));
    }
    private static void UsuarioSemPermissao(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ResponseError("Usuario sem permissão de acesso."));
    }
}