using livro_receitas.Comunicacao.Request;

namespace livro_receitas.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioUserCase
{
    public Task Executar(RequestRegistrarUsuarioJson request)
    {
        throw new Exception(); // retirar
    }

    private void Validar(RequestRegistrarUsuarioJson request)
    {
        var validator = new RegistrarUsuarioValidator();
        var resultado = validator.Validate(request);

        if (!resultado.IsValid)
        {
            var msgError = resultado.Errors.Select(e => e.ErrorMessage);
            throw new Exception();
        }
    }
}