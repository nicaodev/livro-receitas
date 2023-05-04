using livro_receitas.Application.Services.Token;
using livro_receitas.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace livro_receitas.Api.Filter.UsuarioLogado;

public class UsuarioLogadoHandler : AuthorizationHandler<UsuarioLogadoRequirement>
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepository _usuarioReadOnlyRepository;

    public UsuarioLogadoHandler(IHttpContextAccessor contextAccessor, TokenController tokenController, IUsuarioReadOnlyRepository usuarioReadOnlyRepository)
    {
        _contextAccessor = contextAccessor;
        _tokenController = tokenController;
        _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UsuarioLogadoRequirement requirement)
    {
        try
        {
            var authorization = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorization))
            {
                context.Fail();
                return;
            }

            var token = authorization["Bearer".Length..].Trim();
            var emailUser = _tokenController.RecuperarEmail(token);

            var usuario = await _usuarioReadOnlyRepository.RecuperarPorEmail(emailUser);

            if (usuario is null)
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
        catch
        {
            context.Fail();
        }
    }
}