﻿using AutoMapper;
using livro_receitas.Application.Services.UsuarioLogado;
using livro_receitas.Application.UseCases.Receita.Registrar;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Domain.Repositories;
using livro_receitas.Domain.Repositories.Receita;
using livro_receitas.Exceptions.ExceptionsBase;

namespace livro_receitas.Application.UseCases.Receita.Atualizar;

public class AtualizarReceitaUseCase : IAtualizarReceitaUseCase
{
    private readonly IUpdateOnlyRepository _repository;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IMapper _mapper;
    private readonly IUnityOfWork _unityOfWork;

    public AtualizarReceitaUseCase(IUpdateOnlyRepository repository, IUsuarioLogado usuarioLogado, IMapper mapper, IUnityOfWork wow)
    {
        _repository = repository;
        _usuarioLogado = usuarioLogado;
        _mapper = mapper;
        _unityOfWork = wow;
    }

    public async Task Executar(long id, RequestReceitaJson request)
    {
        var userLogado = await _usuarioLogado.RecuperarUser();

        var receita = await _repository.RecuperarPorId(id);

        Validar(userLogado, receita, request);
        _mapper.Map(request, receita);

        _repository.Update(receita);

        await _unityOfWork.Commit();
    }


    private void Validar(Domain.Entidades.Usuario userLogado, Domain.Entidades.Receita receita, RequestReceitaJson request)
    {
        if (receita is null || receita.UsuarioId != userLogado.Id)
            throw new ErroValidacaoException(new List<string> { "Produto não encontrado." });

        var validator = new AtualizarReceitaValidator();

        var resultado = validator.Validate(request);

        if (!resultado.IsValid)
        {
            var msg = resultado.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ErroValidacaoException(msg);
        }
    }
}