﻿using livro_receitas.Comunicacao.Response;
using livro_receitas.Exceptions;
using livro_receitas.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace livro_receitas.Api.Filter;

public class FilterExceptions : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is LivroReceitasException)
        {
            TratarLivroReceitasException(context);
        }
        else
        {
            LancaErroDesconhecido(context);
        }
    }

    private void TratarLivroReceitasException(ExceptionContext context)
    {
        if (context.Exception is ErroValidacaoException)
        {
            TratarErroValidacaoException(context);
        }
        else if (context.Exception is LoginInvalidoException)
        {
            TratarErroLoginException(context);
        }
    }

    private void TratarErroValidacaoException(ExceptionContext context)
    {
        var erroValidacaoException = context.Exception as ErroValidacaoException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ResponseError(erroValidacaoException.MensagensDeErro));
    }

    private void TratarErroLoginException(ExceptionContext context)
    {
        var erroLogin = context.Exception as LoginInvalidoException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Result = new ObjectResult(new ResponseError(erroLogin.Message));
    }

    private void LancaErroDesconhecido(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseError(ResourceMensagensDeErro.ERRO_DESCONHECIDO));
    }
}