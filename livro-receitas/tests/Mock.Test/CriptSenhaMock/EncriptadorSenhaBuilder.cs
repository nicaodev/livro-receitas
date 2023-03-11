using livro_receitas.Application.Services.Criptografia;

namespace Mock.Test.CriptSenhaMock;

public class EncriptadorSenhaBuilder
{

    public static EncriptadorSenha Instancia()
    {
        return new EncriptadorSenha("ABCDEFGHIJKLM-NicolasAlexandre-Developer.");
    }
}