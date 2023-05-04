using AutoMapper;
using HashidsNet;
using livro_receitas.Application.Services.UsuarioLogado;
using livro_receitas.Domain.Entidades;
using livro_receitas.Domain.Repositories;
using livro_receitas.Domain.Repositories.Codigo;
using livro_receitas.Domain.Repositories.Receita;

namespace livro_receitas.Application.UseCases.Conexao.GerarQrCode;

public class GerarQrCodeUseCase : IGerarQrCodeUseCase
{
    private readonly ICodigoWriteOnlyRepository _repository;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IUnityOfWork _wow;
    private readonly IHashids _hashids;

    public GerarQrCodeUseCase(ICodigoWriteOnlyRepository repository, IUsuarioLogado usuarioLogado, IUnityOfWork wow, IHashids hashids)
    {
        _repository = repository;
        _usuarioLogado = usuarioLogado;
        _wow = wow;
        _hashids = hashids;
    }

    public async Task<string> Executar()
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUser();

        var codigo = new Codigos
        {
            Codigo = Guid.NewGuid().ToString(),
            UsuarioId = usuarioLogado.Id
        };

        await _repository.Registrar(codigo);
        await _wow.Commit();

        return codigo.Codigo;
    }
}