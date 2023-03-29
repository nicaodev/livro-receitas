using Bogus;
using livro_receitas.Domain.Entidades;
using Mock.Test.CriptSenhaMock;

namespace Mock.Test.EntidadesMock;

public class UsuarioBuilder
{
    public static (Usuario usuario, string senha) Construir() // func return 2 values
    {
        string senha = string.Empty;

        var usuarioGerado = new Faker<Usuario>()
            .RuleFor(c => c.Id, _ => 1)
            .RuleFor(c => c.Nome, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Senha, f =>
            {
                senha = f.Internet.Password();
                return  EncriptadorSenhaBuilder.Instancia().Criptografar(senha);
            })
            .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"));

        return (usuarioGerado, senha);
    }
}