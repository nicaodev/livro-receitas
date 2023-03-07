namespace livro_receitas.Comunicacao.Response;

public class ResponseError
{
    public List<string> Mensagens { get; set; }

    public ResponseError(string mensagem)
    {
        Mensagens = new List<string>
        {
            mensagem
        };
    }
    public ResponseError(List<string> mensagens)
    {
        Mensagens = mensagens;
    }
}