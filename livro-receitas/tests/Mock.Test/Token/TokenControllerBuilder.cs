using livro_receitas.Application.Services.Token;
using System.Text;

namespace Mock.Test.Token;

public class TokenControllerBuilder
{
    public static TokenController Instancia()
    {
        return new TokenController(10, EncodeToBase64("%jCL08uq9AYcM!6WBP%7fmnQ25a9n8cHA"));
    }

    static public string EncodeToBase64(string texto)
    {
        try
        {
            byte[] textoAsBytes = Encoding.ASCII.GetBytes(texto);
            string resultado = System.Convert.ToBase64String(textoAsBytes);
            return resultado;
        }
        catch (Exception)
        {
            throw;
        }
    }

}