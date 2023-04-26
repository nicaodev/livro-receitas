using FluentValidation;
using livro_receitas.Comunicacao.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livro_receitas.Application.UseCases.Receita.Atualizar;
public class AtualizarReceitaValidator : AbstractValidator<RequestReceitaJson>
{
    public AtualizarReceitaValidator()
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
