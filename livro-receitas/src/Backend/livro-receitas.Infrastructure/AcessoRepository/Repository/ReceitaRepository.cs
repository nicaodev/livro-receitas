using livro_receitas.Domain.Entidades;
using livro_receitas.Domain.Repositories.Receita;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livro_receitas.Infrastructure.AcessoRepository.Repository;
public class ReceitaRepository : IReceitaWriteOnlyRepository
{
    private readonly LivroReceitasContext _context;

    public ReceitaRepository(LivroReceitasContext context)
    {
        _context = context;
    }

    public async Task Registrar(Receita receita)
    {
        await _context.Receitas.AddAsync(receita);
    }
}
