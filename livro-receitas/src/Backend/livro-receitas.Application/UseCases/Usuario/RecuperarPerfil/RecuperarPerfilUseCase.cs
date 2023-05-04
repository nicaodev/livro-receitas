using AutoMapper;
using livro_receitas.Application.Services.UsuarioLogado;
using livro_receitas.Comunicacao.Response;

namespace livro_receitas.Application.UseCases.Usuario.RecuperarPerfil;

internal class RecuperarPerfilUseCase : IRecuperarPerfilUseCase
{
    private readonly IMapper _mapper;
    private readonly IUsuarioLogado _usuarioLogado;

    public RecuperarPerfilUseCase(IMapper mapper, IUsuarioLogado usuarioLogado)
    {
        _mapper = mapper;
        _usuarioLogado = usuarioLogado;
    }

    public async Task<ResponsePerfilUsuarioJson> Executar()
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUser();

        return _mapper.Map<ResponsePerfilUsuarioJson>(usuarioLogado);
    }
}