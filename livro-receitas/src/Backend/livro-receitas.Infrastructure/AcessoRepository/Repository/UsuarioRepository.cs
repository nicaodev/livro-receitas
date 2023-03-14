using livro_receitas.Domain.Entidades;
using livro_receitas.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace livro_receitas.Infrastructure.AcessoRepository.Repository;

public class UsuarioRepository : IUsuarioReadOnlyRepository, IUsuarioWriteOnlyRepository
{
    private readonly LivroReceitasContext _context;

    public UsuarioRepository(LivroReceitasContext context)
    {
        _context = context;
    }

    public async Task Adicionar(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
    }

    public async Task<bool> ExisteUsuario(string email)
    {
        return await _context.Usuarios.AnyAsync(c => c.Email.Equals(email));
    }

    public async Task<Usuario> Login(string email, string senha)
    {
        return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Senha.Equals(senha));
    }
}