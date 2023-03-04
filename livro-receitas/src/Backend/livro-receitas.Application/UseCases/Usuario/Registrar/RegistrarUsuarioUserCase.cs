using AutoMapper;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Domain.Repositories;
using livro_receitas.Exceptions.ExceptionsBase;

namespace livro_receitas.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioUserCase : IRegistrarUsuarioUserCase
{
    private readonly IUsuarioWriteOnlyRepository _repositorio;
    private readonly IMapper _mapper;
    private readonly IUnityOfWork _unityOfWork;
    public RegistrarUsuarioUserCase(IUsuarioWriteOnlyRepository repository, IMapper mapper, IUnityOfWork unityOfWork)
    {
        _repositorio = repository;
        _mapper = mapper;
        _unityOfWork = unityOfWork;

    }

    public async Task Executar(RequestRegistrarUsuarioJson request)
    {
        Validar(request);
        var entidade = _mapper.Map<Domain.Entidades.Usuario>(request);

        entidade.Senha = "cript";

        await _repositorio.Adicionar(entidade);

        await _unityOfWork.Commit();
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