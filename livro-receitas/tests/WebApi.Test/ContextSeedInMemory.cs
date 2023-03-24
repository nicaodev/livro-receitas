using livro_receitas.Infrastructure.AcessoRepository;
using Mock.Test.EntidadesMock;

namespace WebApi.Test;

public class ContextSeedInMemory
{
    public static (livro_receitas.Domain.Entidades.Usuario user, string senha) Seed(LivroReceitasContext context)
    {
        (var user, string senha) = UsuarioBuilder.Construir();

        context.Usuarios.Add(user);

        context.SaveChanges();

        return (user, senha);
    }
}