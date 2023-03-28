﻿using FluentValidation;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Exceptions;

namespace livro_receitas.Application.UseCases.Usuario;

public class SenhaValidator : AbstractValidator<string>
{
    public SenhaValidator()
    {
        RuleFor(senha => senha).NotEmpty().WithMessage(ResourceMensagensDeErro.SENHA_USUARIO_EM_BRANCO);
        When(senha => !string.IsNullOrWhiteSpace(senha), () =>
        {
            RuleFor(senha => senha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMensagensDeErro.SENHA_USUARIO_MINIMO_SEIS_CARACTERES);
        });
    }
}