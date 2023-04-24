using AutoMapper;
using livro_receitas.Application.Services.UsuarioLogado;
using livro_receitas.Comunicacao.Response;
using livro_receitas.Domain.Repositories;
using livro_receitas.Domain.Repositories.Receita;
using livro_receitas.Exceptions.ExceptionsBase;

namespace livro_receitas.Application.UseCases.Receita.Deletar;

public class DeletarReceitaUseCase : IDeletarReceitaUseCase
{
    private readonly IReceitaReadOnlyRepository _repositoryRead;
    private readonly IReceitaWriteOnlyRepository _repositoryWrite;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IUnityOfWork _WoW;

    public DeletarReceitaUseCase(IReceitaReadOnlyRepository repositoryRead, IUsuarioLogado usuarioLogado, IReceitaWriteOnlyRepository repositoryWrite, IUnityOfWork Wow)
    {
        _repositoryRead = repositoryRead;
        _usuarioLogado = usuarioLogado;
        _repositoryWrite = repositoryWrite;
        _WoW = Wow;
    }

    public async Task Executar(long id)
    {
        var userLogado = await _usuarioLogado.RecuperarUser();

        var receita = await _repositoryRead.RecuperarPorId(id);

        Validar(userLogado, receita);

        await _repositoryWrite.Deletar(id);
        await _WoW.Commit();
    }

    private void Validar(Domain.Entidades.Usuario userLogado, Domain.Entidades.Receita receita)
    {
        if (receita == null || receita.UsuarioId != userLogado.Id)
            throw new ErroValidacaoException(new List<string> { "Produto não encontrado." });
    }
}