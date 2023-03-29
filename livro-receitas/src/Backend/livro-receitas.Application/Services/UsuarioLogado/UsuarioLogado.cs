using livro_receitas.Application.Services.Token;
using livro_receitas.Domain.Entidades;
using livro_receitas.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace livro_receitas.Application.Services.UsuarioLogado;

public class UsuarioLogado : IUsuarioLogado
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepository _usuarioReadOnlyRepository;

    public UsuarioLogado(IHttpContextAccessor httpContextAccessor, TokenController tokenController, IUsuarioReadOnlyRepository usuarioReadOnlyRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenController = tokenController;
        _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
    }
    public async Task<Usuario> RecuperarUser()
    {
        var auth = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        var token = auth["Bearer".Length..].Trim();

        var emailUser = _tokenController.RecuperarEmail(token);

        var usuario = await _usuarioReadOnlyRepository.RecuperarPorEmail(emailUser);

        return usuario;
    }
}