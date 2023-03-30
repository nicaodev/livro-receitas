using Bogus;
using livro_receitas.Comunicacao.Request;

namespace Mock.Test.Requests;

public class RequestAlterarSenhaUsuarioBuilder
{
    public static RequestAlterarSenhaJson Construir(int tamSenha = 10)
    {
        return new Faker<RequestAlterarSenhaJson>()
            .RuleFor(s => s.SenhaAtual, f => f.Internet.Password(10))
            .RuleFor(s => s.NovaSenha, f => f.Internet.Password(tamSenha));
    }
}