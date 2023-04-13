﻿using livro_receitas.Domain.Entidades;
using livro_receitas.Domain.Repositories.Receita;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livro_receitas.Infrastructure.AcessoRepository.Repository;
public class ReceitaRepository : IReceitaWriteOnlyRepository, IReceitaReadOnlyRepository
{
    private readonly LivroReceitasContext _context;

    public ReceitaRepository(LivroReceitasContext context)
    {
        _context = context;
    }

    public async Task<Receita> RecuperarPorId(long receitdaId)
    {
        return await _context.Receitas.AsNoTracking().Include(i => i.Ingredientes).FirstOrDefaultAsync(x => x.Id == receitdaId);
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
}
