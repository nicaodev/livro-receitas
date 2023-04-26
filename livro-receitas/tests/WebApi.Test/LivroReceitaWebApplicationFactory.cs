using livro_receitas.Infrastructure.AcessoRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test;

public class LivroReceitaWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
{

    private livro_receitas.Domain.Entidades.Usuario _usuario;
    private string _senha;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descritor = services.SingleOrDefault(d => d.ServiceType == typeof(LivroReceitasContext));
                if (descritor is not null)
                    services.Remove(descritor);

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<LivroReceitasContext>(opt =>
                {
                    opt.UseInMemoryDatabase("InMemoryDbForTesting");
                    opt.UseInternalServiceProvider(provider);
                });

                var serviceProvider = services.BuildServiceProvider();

                using var scope = serviceProvider.CreateScope();
                var scopeService = scope.ServiceProvider;

                var database = scopeService.GetRequiredService<LivroReceitasContext>();

                database.Database.EnsureDeleted();


                (_usuario, _senha) = ContextSeedInMemory.Seed(database);
            });
    }

    public livro_receitas.Domain.Entidades.Usuario retornaUsuarioCriadoInMemory()
    {
        return _usuario;
    }

    public string retornaSenhaUsuarioCriadoInMemory()
    {
        return _senha;
    }
}