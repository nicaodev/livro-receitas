using Microsoft.AspNetCore.SignalR;
using System.Timers;

namespace livro_receitas.Api.WebSockets;

public class Conexao
{
    private readonly IHubContext<AddConexao> _hubContext;
    private readonly string UsuarioQueCriouQrCodeConnectionId;
    private Action<string> _callbackTempoExpirado;

    public Conexao(IHubContext<AddConexao> hubContext, string usuarioQueCriouQrCodeConnectionId)
    {
        _hubContext = hubContext;
        UsuarioQueCriouQrCodeConnectionId = usuarioQueCriouQrCodeConnectionId;
    }

    private short tempoRestanteSegundos { get; set; }

    private System.Timers.Timer _timer { get; set; }

    public void IniciarContagemTempo(Action<string> callbackTempoExpirado)
    {

        _callbackTempoExpirado = callbackTempoExpirado;

        tempoRestanteSegundos = 60;

        _timer = new System.Timers.Timer(1000)
        {
            Enabled = false
        };
        _timer.Elapsed += ElapsedTimer;
        _timer.Enabled = true;
    }

    private async void ElapsedTimer(object sender, ElapsedEventArgs e)
    {
        if (tempoRestanteSegundos >= 0)
        {
            await _hubContext.Clients.Client(UsuarioQueCriouQrCodeConnectionId).SendAsync("SetTempoRestante", tempoRestanteSegundos--);
        }
        else
        {
            StopTimer();
            _callbackTempoExpirado(UsuarioQueCriouQrCodeConnectionId);
        }
    }

    public void StopTimer()
    {
        _timer?.Stop();
        _timer?.Dispose();
        _timer = null;
    }
}