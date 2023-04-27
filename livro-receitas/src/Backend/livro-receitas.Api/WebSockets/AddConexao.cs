using livro_receitas.Application.UseCases.Conexao.GerarQrCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace livro_receitas.Api.WebSockets;
[Authorize(Policy = "PoliticaUsuarioLogado")]
public class AddConexao : Hub
{
    private readonly IGerarQrCodeUseCase _gerarQrCodeUseCase;

    public AddConexao(IGerarQrCodeUseCase gerarQrCodeUseCase)
    {
        _gerarQrCodeUseCase = gerarQrCodeUseCase;
    }

    public async Task GetQrCode()
    {
        var qrCode = await _gerarQrCodeUseCase.Executar();
        await Clients.Client(Context.ConnectionId).SendAsync("ResultadoQrCode", qrCode);
    }

    public override Task OnConnectedAsync()
    {
        var x = Context.ConnectionId;

        return base.OnConnectedAsync();
    }
}