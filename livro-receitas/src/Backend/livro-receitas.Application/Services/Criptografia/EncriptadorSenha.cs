using System.Security.Cryptography;
using System.Text;

namespace livro_receitas.Application.Services.Criptografia;

public class EncriptadorSenha
{

    private readonly string _chaveExtraEncriptador;

    public EncriptadorSenha(string chaveExtraEncriptador)
    {
        _chaveExtraEncriptador = chaveExtraEncriptador;
    }
    public string Criptografia(string senha)
    {
        var senhaComChaveAdicional = $"{senha}{_chaveExtraEncriptador}";

        var bytes = Encoding.UTF8.GetBytes(senhaComChaveAdicional);
        var sha512 = SHA512.Create();
        byte[] hashbytes = sha512.ComputeHash(bytes);
        return StringBytes(hashbytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }
}