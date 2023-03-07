namespace livro_receitas.Exceptions.ExceptionsBase;

public class ErroValidacaoException : LivroReceitasException
{

    public List<string> MensagensDeErro { get; set; }

    public ErroValidacaoException(List<string> errors)
    {
        MensagensDeErro = errors;
    }
}