using AutoMapper;
using livro_receitas.Application.Services.Criptografia;
using livro_receitas.Application.Services.Token;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Comunicacao.Response;
using livro_receitas.Domain.Repositories;
using livro_receitas.Exceptions.ExceptionsBase;

namespace livro_receitas.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioUserCase : IRegistrarUsuarioUserCase
{
    private readonly IUsuarioWriteOnlyRepository _repositorio;
    private readonly IMapper _mapper;
    private readonly IUnityOfWork _unityOfWork;
    private readonly EncriptadorSenha _encriptadorSenha;
    private readonly TokenController _tokenController;

    public RegistrarUsuarioUserCase(IUsuarioWriteOnlyRepository repository, IMapper mapper, IUnityOfWork unityOfWork,
        EncriptadorSenha encriptadorSenha, TokenController tokenController)
    {
        _repositorio = repository;
        _mapper = mapper;
        _unityOfWork = unityOfWork;
        _encriptadorSenha = encriptadorSenha;
        _tokenController = tokenController;
    }

    public async Task<ResponseUsuarioRegistradoJson> Executar(RequestRegistrarUsuarioJson request)
    {
        Validar(request);
        var entidade = _mapper.Map<Domain.Entidades.Usuario>(request);

        entidade.Senha = _encriptadorSenha.Criptografia(request.Senha);

        await _repositorio.Adicionar(entidade);

        await _unityOfWork.Commit();

        var token = _tokenController.GerarToken(entidade.Email);

        return new ResponseUsuarioRegistradoJson
        {
            Token = token
        };
    }

    private void Validar(RequestRegistrarUsuarioJson request)
    {
        var validator = new RegistrarUsuarioValidator();
        var resultado = validator.Validate(request);

        if (!resultado.IsValid)
        {
            var msgError = resultado.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErroValidacaoException(msgError);
        }
    }
}