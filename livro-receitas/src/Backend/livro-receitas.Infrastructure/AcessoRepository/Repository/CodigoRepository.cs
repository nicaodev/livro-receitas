using livro_receitas.Domain.Entidades;
using livro_receitas.Domain.Repositories.Codigo;
using Microsoft.EntityFrameworkCore;

namespace livro_receitas.Infrastructure.AcessoRepository.Repository;

public class CodigoRepository : ICodigoWriteOnlyRepository
{
    private readonly LivroReceitasContext _context;

    public CodigoRepository(LivroReceitasContext context)
    {
        _context = context;
    }

    public async Task Registrar(Codigos codigo)
    {
        var codigoBancoDeDados = await _context.Codigos.FirstOrDefaultAsync(c => c.UsuarioId == codigo.UsuarioId);

        if (codigoBancoDeDados is not null)
        {
            codigoBancoDeDados.Codigo = codigo.Codigo;
            _context.Codigos.Update(codigoBancoDeDados);
        }
        else
        {
            await _context.Codigos.AddAsync(codigo);
        }
    }
}