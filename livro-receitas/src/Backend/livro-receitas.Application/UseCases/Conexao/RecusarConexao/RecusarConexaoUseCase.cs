using livro_receitas.Application.Services.UsuarioLogado;
using livro_receitas.Domain.Repositories;
using livro_receitas.Domain.Repositories.Codigo;

namespace livro_receitas.Application.UseCases.Conexao.RecusarConexao;

public class RecusarConexaoUseCase : IRecusarConexaoUseCase
{
    private readonly ICodigoWriteOnlyRepository _repository;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IUnityOfWork _wow;

    public RecusarConexaoUseCase(ICodigoWriteOnlyRepository repository, IUsuarioLogado usuarioLogado, IUnityOfWork wow)
    {
        _repository = repository;
        _usuarioLogado = usuarioLogado;
        _wow = wow;
    }

    public async Task Executar()
    {
        var usuarioLogado = _usuarioLogado.RecuperarUser();

        await _repository.Deletar(usuarioLogado.Id);

        await _wow.Commit();
    }
}