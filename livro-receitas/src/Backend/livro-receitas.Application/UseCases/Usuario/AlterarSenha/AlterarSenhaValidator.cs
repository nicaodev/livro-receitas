using FluentValidation;
using livro_receitas.Comunicacao.Request;

namespace livro_receitas.Application.UseCases.Usuario.AlterarSenha;

public class AlterarSenhaValidator : AbstractValidator<RequestAlterarSenhaJson>
{
    public AlterarSenhaValidator()
    {
        RuleFor(c => c.NovaSenha).SetValidator(new SenhaValidator());
    }
}