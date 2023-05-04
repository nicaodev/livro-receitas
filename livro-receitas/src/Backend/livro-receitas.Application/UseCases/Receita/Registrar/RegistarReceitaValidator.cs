using FluentValidation;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Domain.Extensions;

namespace livro_receitas.Application.UseCases.Receita.Registrar;

public class RegistarReceitaValidator : AbstractValidator<RequestReceitaJson>
{
    public RegistarReceitaValidator()
    {
        RuleFor(x => x.Titulo).NotEmpty();
        RuleFor(x => x.Categoria).IsInEnum();
        RuleFor(x => x.ModoPreparo).NotEmpty();
        RuleFor(x => x.Ingredientes).NotEmpty();
        RuleFor(x => x.TempoPreparo).InclusiveBetween(1, 500);
        RuleForEach(x => x.Ingredientes).ChildRules(ingrediente =>
        {
            ingrediente.RuleFor(x => x.Produto).NotEmpty();
            ingrediente.RuleFor(x => x.Quantidade).NotEmpty();
        });

        RuleFor(x => x.Ingredientes).Custom((ingredientes, contexto) =>
        {
            var produtosDistintos = ingredientes.Select(c => c.Produto.RemoverAcentos().ToLower()).Distinct();
            if (produtosDistintos.Count() != ingredientes.Count())
            {
                contexto.AddFailure(new FluentValidation.Results.ValidationFailure("Ingredientes", "Há valores repetidos."));
            }
        });
    }
}