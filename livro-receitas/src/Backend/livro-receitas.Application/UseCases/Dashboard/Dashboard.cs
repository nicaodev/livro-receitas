using AutoMapper;
using livro_receitas.Application.Services.UsuarioLogado;
using livro_receitas.Comunicacao.Enum;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Comunicacao.Response;
using livro_receitas.Domain.Repositories.Receita;

namespace livro_receitas.Application.UseCases.Dashboard;

public class Dashboard : IDashboard
{
    private readonly IReceitaReadOnlyRepository _repository;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IMapper _mapper;

    public Dashboard(IReceitaReadOnlyRepository repository, IUsuarioLogado usuarioLogado, IMapper mapper)
    {
        _repository = repository;
        _usuarioLogado = usuarioLogado;
        _mapper = mapper;
    }

    public async Task<ResponseDashboardJson> Executar(RequestDashboardJson request)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUser();

        var receitas = await _repository.RecuperarTodasDoUsuario(usuarioLogado.Id);

        receitas = Filtrar(request, receitas);

        return new ResponseDashboardJson
        {
            Receitas = _mapper.Map<List<ResponseReceitasDashboardJson>>(receitas)
        };
    }

    private static IList<Domain.Entidades.Receita> Filtrar(RequestDashboardJson request, IList<Domain.Entidades.Receita> receitas)
    {
        var receitasFiltradas = receitas;
        if (request.Categoria.HasValue)
        {
            receitasFiltradas = receitas.Where(r => r.Categoria == (Domain.Enum.Categoria)request.Categoria.Value).ToList();
        }

        if (!string.IsNullOrWhiteSpace(request.TituloOuIngrediente))
        {
            receitasFiltradas = receitas.Where(r => r.Titulo.Contains(request.TituloOuIngrediente)
            ||
            r.Ingredientes.Any(ingre => ingre.Produto.Contains(request.TituloOuIngrediente))).ToList();
        }
        return receitasFiltradas;
    }
}