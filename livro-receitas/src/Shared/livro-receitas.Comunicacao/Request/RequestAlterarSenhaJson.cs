namespace livro_receitas.Comunicacao.Request;

public class RequestAlterarSenhaJson
{
    public string SenhaAtual { get; set; }
    public string NovaSenha { get; set; }
}