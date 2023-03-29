using livro_receitas.Domain.Entidades;
using livro_receitas.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace livro_receitas.Infrastructure.AcessoRepository.Repository;

public class UsuarioRepository : IUsuarioReadOnlyRepository, IUsuarioWriteOnlyRepository, IUsuarioUpdateOnlyRepository
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

    public async Task<Usuario> RecuperarPorEmail(string email)
    {
        return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(s => s.Email.Equals(email));
    }

    public async Task<Usuario> RecuperarPorEmailESenha(string email, string senha)
    {
        return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Senha.Equals(senha));
    }

    public async Task<Usuario> RecuperarPorId(long id)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(c => c.Id == id);
    }

    public void Update(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
    }
}