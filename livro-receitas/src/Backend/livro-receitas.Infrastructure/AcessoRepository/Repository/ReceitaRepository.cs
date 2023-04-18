using livro_receitas.Domain.Entidades;
using livro_receitas.Domain.Repositories.Receita;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livro_receitas.Infrastructure.AcessoRepository.Repository;
public class ReceitaRepository : IReceitaWriteOnlyRepository, IReceitaReadOnlyRepository, IUpdateOnlyRepository
{
    private readonly LivroReceitasContext _context;

    public ReceitaRepository(LivroReceitasContext context)
    {
        _context = context;
    }

    async Task<Receita> IReceitaReadOnlyRepository.RecuperarPorId(long receitdaId)
    {
        return await _context.Receitas.AsNoTracking().Include(i => i.Ingredientes).FirstOrDefaultAsync(x => x.Id == receitdaId);
    }

    async Task<Receita> IUpdateOnlyRepository.RecuperarPorId(long receitdaId)
    {
        return await _context.Receitas.Include(i => i.Ingredientes).FirstOrDefaultAsync(x => x.Id == receitdaId);
    }

    public async Task<IList<Receita>> RecuperarTodasDoUsuario(long idUsuario)
    {
        return await _context.Receitas.AsNoTracking().
            Include(r => r.Ingredientes).
            Where(r => r.UsuarioId == idUsuario).ToListAsync();
    }

    public async Task Registrar(Receita receita)
    {
        await _context.Receitas.AddAsync(receita);
    }

    public void Update(Receita receita)
    {
        _context.Receitas.Update(receita);
    }
}
