using livro_receitas.Application.Services.Criptografia;
using livro_receitas.Application.Services.UsuarioLogado;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Domain.Repositories;
using livro_receitas.Exceptions;
using livro_receitas.Exceptions.ExceptionsBase;
using System.Net.Http.Headers;

namespace livro_receitas.Application.UseCases.Usuario.AlterarSenha;

internal class AlterarSenhaUseCase : IAlterarSenhaUseCase
{
    private IUsuarioUpdateOnlyRepository _repo;
    private IUsuarioLogado _userLogado;

    private readonly EncriptadorSenha _encriptadorSenha;

    public AlterarSenhaUseCase(IUsuarioUpdateOnlyRepository repo, IUsuarioLogado userLogado, EncriptadorSenha encriptadorSenha)
    {
        _repo = repo;
        _userLogado = userLogado;
        _encriptadorSenha = encriptadorSenha;
    }

    public async Task Executar(RequestAlterarSenhaJson request)
    {
        var userLogado = await _userLogado.RecuperarUser();

        var usuario = await _repo.RecuperarPorId(userLogado.Id);

        Validar(request, usuario);

        usuario.Senha = _encriptadorSenha.Criptografar(request.NovaSenha);

        _repo.Update(usuario);
    }

    private void Validar(RequestAlterarSenhaJson request, Domain.Entidades.Usuario usuario)
    {
        var validator = new AlterarSenhaValidator();
        var resultado = validator.Validate(request);

        var senhaAtualCriptografada = _encriptadorSenha.Criptografar(request.SenhaAtual);

        if (!usuario.Senha.Equals(senhaAtualCriptografada))
        {
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("senhaAtual", "Senha Atual Inválida"));
        }


        if (!resultado.IsValid)
        {
            var msgs = resultado.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErroValidacaoException(msgs);
        }
    }
}