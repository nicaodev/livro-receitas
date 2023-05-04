using livro_receitas.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace livro_receitas.Infrastructure.AcessoRepository;

public class LivroReceitasContext : DbContext
{
    public LivroReceitasContext(DbContextOptions<LivroReceitasContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Receita> Receitas{ get; set; }
    public DbSet<Codigos> Codigos{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LivroReceitasContext).Assembly);
    }
}