using AutoMapper;
using livro_receitas.Application.Services.UsuarioLogado;
using livro_receitas.Comunicacao.Response;
using livro_receitas.Domain.Repositories.Receita;
using livro_receitas.Exceptions.ExceptionsBase;

namespace livro_receitas.Application.UseCases.Receita.RecuperarPorId;

public class RecuperarReceitaPorIdUseCase : IRecuperarReceitaPorIdUseCase
{
    private readonly IReceitaReadOnlyRepository _repository;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IMapper _mapper;

    public RecuperarReceitaPorIdUseCase(IReceitaReadOnlyRepository repository, IUsuarioLogado usuarioLogado, IMapper mapper)
    {
        _repository = repository;
        _usuarioLogado = usuarioLogado;
        _mapper = mapper;
    }
    public async Task<ResponseReceitaJson> Executar(long id)
    {
        var userLogado = await _usuarioLogado.RecuperarUser();

        var receita = await _repository.RecuperarPorId(id);

        Validar(userLogado, receita);

        return _mapper.Map<ResponseReceitaJson>(receita);
    }

    private void Validar(Domain.Entidades.Usuario userLogado, Domain.Entidades.Receita receita)
    {
        if (receita is null || receita.UsuarioId != userLogado.Id)
            throw new ErroValidacaoException(new List<string> { "Produto não encontrado." });
    }
}