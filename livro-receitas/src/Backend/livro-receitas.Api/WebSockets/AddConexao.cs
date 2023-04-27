using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace livro_receitas.Api.WebSockets;
[Authorize(Policy = "PoliticaUsuarioLogado")]
public class AddConexao : Hub
{
    public async Task GetQrCode()
    {
        var qrCode = "ABCDE";
        await Clients.Client(Context.ConnectionId).SendAsync("ResultadoQrCode", qrCode);
    }

    public override Task OnConnectedAsync()
    {
        var x = Context.ConnectionId;

        return base.OnConnectedAsync();
    }
}