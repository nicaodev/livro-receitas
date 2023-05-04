using livro_receitas.Application.UseCases.Conexao.GerarQrCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace livro_receitas.Api.WebSockets;

[Authorize(Policy = "PoliticaUsuarioLogado")]
public class AddConexao : Hub
{
    private Broadcaster _broadcaster;
    private readonly IGerarQrCodeUseCase _gerarQrCodeUseCase;
    private readonly IHubContext<AddConexao> _hubContext;

    public AddConexao(IHubContext<AddConexao> hubContext, IGerarQrCodeUseCase gerarQrCodeUseCase)
    {
        _broadcaster = Broadcaster.Instance;
        _gerarQrCodeUseCase = gerarQrCodeUseCase;
        _hubContext = hubContext;
    }

    public async Task GetQrCode()
    {
        var qrCode = await _gerarQrCodeUseCase.Executar();

        _broadcaster.InicializarConexao(_hubContext, Context.ConnectionId);

        await Clients.Client(Context.ConnectionId).SendAsync("ResultadoQrCode", qrCode);
    }

    public override Task OnConnectedAsync()
    {
        var x = Context.ConnectionId;

        return base.OnConnectedAsync();
    }

    public async Task RecurarConexao()
    {
        var connectionIdUsuarioQueGerouQrCode = Context.ConnectionId;
    }
}