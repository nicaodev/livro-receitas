using livro_receitas.Domain.Repositories;

namespace livro_receitas.Infrastructure.AcessoRepository;

public sealed class UnityOfWork : IDisposable, IUnityOfWork
{
    private readonly LivroReceitasContext _context;
    private bool _disposed;

    public UnityOfWork(LivroReceitasContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }
}