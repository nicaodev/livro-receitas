using livro_receitas.Comunicacao.Request;
using livro_receitas.Exceptions.ExceptionsBase;

namespace livro_receitas.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioUserCase
{
    public async Task Executar(RequestRegistrarUsuarioJson request)
    {
        Validar(request);

        // Salvar no Banco de dados...
    }

    private void Validar(RequestRegistrarUsuarioJson request)
    {
        var validator = new RegistrarUsuarioValidator();
        var resultado = validator.Validate(request);

        if (!resultado.IsValid)
        {
            var msgError = resultado.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErroValidacaoException(msgError);
        }
    }
}