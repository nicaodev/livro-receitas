using FluentAssertions;
using livro_receitas.Application.UseCases.Usuario.AlterarSenha;
using livro_receitas.Application.UseCases.Usuario.Registrar;
using livro_receitas.Comunicacao.Request;
using Mock.Test.Requests;
using Xunit;

namespace Validators.Test.Usuario.Registrar;

public class AlterarSenhaValidatorTest
{
    [Fact]
    public void Validar_Sucesso()
    {
        var validator = new AlterarSenhaValidator();

        var requisicao = RequestAlterarSenhaUsuarioBuilder.Construir();
        var resultado = validator.Validate(requisicao);
        resultado.IsValid.Should().BeTrue();
    }
}
