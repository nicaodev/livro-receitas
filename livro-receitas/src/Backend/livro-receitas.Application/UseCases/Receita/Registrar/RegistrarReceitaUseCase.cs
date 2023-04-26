using AutoMapper;
using livro_receitas.Application.Services.UsuarioLogado;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Comunicacao.Response;
using livro_receitas.Domain.Entidades;
using livro_receitas.Domain.Repositories;
using livro_receitas.Domain.Repositories.Receita;
using livro_receitas.Exceptions.ExceptionsBase;

namespace livro_receitas.Application.UseCases.Receita.Registrar;

public class RegistrarReceitaUseCase : IRegistrarReceitaUseCase
{
    private IMapper _mapper;
    private IUnityOfWork _unityOfWork;
    private IUsuarioLogado _usuarioLogado;
    private IReceitaWriteOnlyRepository _receitaWriteOnlyRepository;

    public RegistrarReceitaUseCase(IMapper mapper, IUnityOfWork unityOfWork, IUsuarioLogado usuarioLogado, IReceitaWriteOnlyRepository receitaWriteOnlyRepository)
    {
        _mapper = mapper;
        _unityOfWork = unityOfWork;
        _usuarioLogado = usuarioLogado;
        _receitaWriteOnlyRepository = receitaWriteOnlyRepository;
    }

    public async Task<ResponseReceitaJson> Executar(RequestReceitaJson request)
    {
        Validar(request);
        var userLogado = await _usuarioLogado.RecuperarUser();

        var receita = _mapper.Map<Domain.Entidades.Receita>(request);

        receita.UsuarioId = userLogado.Id;


        await _receitaWriteOnlyRepository.Registrar(receita);

        await _unityOfWork.Commit();

        return _mapper.Map<ResponseReceitaJson>(request); ;
    }

    private void Validar(RequestReceitaJson request)
    {
        var validator = new RegistarReceitaValidator();

        var resultado = validator.Validate(request);

        if (!resultado.IsValid)
        {
            var msg = resultado.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ErroValidacaoException(msg);
        }
    }
}