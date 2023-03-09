using FluentAssertions;
using livro_receitas.Application.UseCases.Usuario.Registrar;
using livro_receitas.Comunicacao.Request;
using livro_receitas.Exceptions;
using Mock.Test.Requests;
using Xunit;

namespace Validators.Test.Usuario.Registrar;

public class RegistrarUsuarioValidatorTest
{
    [Fact]
    public void Validar_Sucesso()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequestRegistrarUsuarioMock.Construir();
        var resultado = validator.Validate(requisicao);
        resultado.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validar_Erro_Nome_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequestRegistrarUsuarioMock.Construir();
        requisicao.Nome = "";
        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals("The user's name is required."/*ResourceMensagensDeErro.NOME_USUARIO_EM_BRANCO*/));
    }
}