namespace livro_receitas.Exceptions.ExceptionsBase;

public class LoginInvalidoException: LivroReceitasException
{
    public LoginInvalidoException() : base(ResourceMensagensDeErro.LOGIN_INVALIDO)
    {
        
    }
}