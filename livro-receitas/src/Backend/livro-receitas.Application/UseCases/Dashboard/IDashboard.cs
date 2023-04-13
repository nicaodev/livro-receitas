using livro_receitas.Comunicacao.Request;
using livro_receitas.Comunicacao.Response;

namespace livro_receitas.Application.UseCases.Dashboard;

public interface IDashboard
{
    Task<ResponseDashboardJson> Executar(RequestDashboardJson request);
}