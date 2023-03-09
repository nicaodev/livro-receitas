using Bogus;
using livro_receitas.Comunicacao.Request;

namespace Mock.Test.Requests
{
    public class RequestRegistrarUsuarioMock
    {
        public static RequestRegistrarUsuarioJson Construir()
        {
            return new Faker<RequestRegistrarUsuarioJson>()
                .RuleFor(c => c.Nome, f => f.Person.FullName)
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Senha, f => f.Internet.Password())
                .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"));
        }
    }
}