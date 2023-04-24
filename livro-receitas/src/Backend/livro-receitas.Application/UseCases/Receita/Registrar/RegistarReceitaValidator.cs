using FluentValidation;
using livro_receitas.Comunicacao.Request;

namespace livro_receitas.Application.UseCases.Receita.Registrar;

public class RegistarReceitaValidator : AbstractValidator<RequestRegistarReceitaJson>
{
    public RegistarReceitaValidator()
    {
        RuleFor(x => x.Titulo).NotEmpty();
        RuleFor(x => x.Categoria).IsInEnum();
        RuleFor(x => x.ModoPreparo).NotEmpty();
        RuleFor(x => x.Ingredientes).NotEmpty();
        RuleForEach(x => x.Ingredientes).ChildRules(ingrediente =>
        {
            ingrediente.RuleFor(x => x.Produto).NotEmpty();
            ingrediente.RuleFor(x => x.Quantidade).NotEmpty();
        });

        RuleFor(x => x.Ingredientes).Custom((ingredientes, contexto) =>
        {
            var produtosDistintos = ingredientes.Select(c => c.Produto).Distinct();
            if (produtosDistintos.Count() != ingredientes.Count())
            {
                contexto.AddFailure(new FluentValidation.Results.ValidationFailure("Ingredientes", "Há valores repetidos."));
            }
        });
    }
}