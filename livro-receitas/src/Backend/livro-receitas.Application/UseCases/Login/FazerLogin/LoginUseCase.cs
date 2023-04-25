using livro_receitas.Application.Services.Criptografia;
using livro_receitas.Application.Services.Token;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Comunicacao.Response;
using livro_receitas.Domain.Repositories;
using livro_receitas.Exceptions.ExceptionsBase;

namespace livro_receitas.Application.UseCases.Login.FazerLogin;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUsuarioReadOnlyRepository _usuarioReadOnlyRepository;
    private readonly EncriptadorSenha _encriptadorSenha;
    private readonly TokenController _tokenController;

    public LoginUseCase(IUsuarioReadOnlyRepository usuarioReadOnlyRepository, EncriptadorSenha encriptadorSenha, TokenController tokenController)
    {
        _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
        _encriptadorSenha = encriptadorSenha;
        _tokenController = tokenController;
    }

    public async Task<ResponseLoginJson> Executar(RequestLoginJson request)
    {
        var criptSenha = _encriptadorSenha.Criptografar(request.Senha);

        var user = await _usuarioReadOnlyRepository.RecuperarPorEmailESenha(request.Email, criptSenha);

        if (user is null)
            throw new LoginInvalidoException();

        return new ResponseLoginJson
        {
            Nome = user.Nome,
            Token = _tokenController.GerarToken(user.Email)
        };
    }
}