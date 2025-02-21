using Microsoft.EntityFrameworkCore.Storage;
using ZeroStoreApp.Domain.Repositories;
using ZeroStoreApp.Domain.Services;

namespace ZeroStoreApp.Infra.Services;

public class UnitOfWork : IUnitOfWork
{
    private ZeroStoreAppDbContext? _context;
    private readonly Lazy<IProductRepository> _lazyProductRepository;

    private IDbContextTransaction? _contextTransaction;

    public UnitOfWork(ZeroStoreAppDbContext context,
        Lazy<IProductRepository> lazyProductRepository)
    {
        _context = context;
        _lazyProductRepository = lazyProductRepository;
    }

    public IProductRepository Products => _lazyProductRepository.Value ?? throw new ArgumentNullException(nameof(IProductRepository));

    public async Task BegingTransactionAsync(CancellationToken cancellationToken)
    {
        if (_context is not null)
            _contextTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        if (_contextTransaction is null) return;
        await _contextTransaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        if (_contextTransaction is null) return;
        await _contextTransaction.RollbackAsync(cancellationToken);
    }

    #region Dispose
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_contextTransaction is not null)
            {
                _contextTransaction.Dispose();
                _contextTransaction = null;
            }
            if (_lazyProductRepository.IsValueCreated && _lazyProductRepository.Value is IDisposable disposable)
            {
                disposable.Dispose();
            }
            if (_context is not null && _context is IDisposable disposableContext)
            {
                disposableContext.Dispose();
                _context = null;
            }
        }
    }
    #endregion
}

public class UnitOfQuery : IUnitOfQuery
{
    private readonly Lazy<IQueryProductRepRepository> _lazyProductRepository;

    public UnitOfQuery(Lazy<IQueryProductRepRepository> lazyProductRepository)
    {
        _lazyProductRepository = lazyProductRepository;
    }

    public IQueryProductRepRepository Products => _lazyProductRepository.Value;

    #region Dispose
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_lazyProductRepository.IsValueCreated && _lazyProductRepository.Value is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
    #endregion
}
