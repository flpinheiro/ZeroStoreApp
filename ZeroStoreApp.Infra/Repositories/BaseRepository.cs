using Microsoft.EntityFrameworkCore;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Repositories;

namespace ZeroStoreApp.Infra.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ZeroStoreAppDbContext _context;
    protected bool _disposed = false;
    public BaseRepository(ZeroStoreAppDbContext context)
    {
        _context = context;
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null) return;

        entity.Delete();
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context
            .Set<TEntity>()
            .Where(e => !e.IsDeleted)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        entity.Update();
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    #region Dispose
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing && !_disposed)
        {
            _context.Dispose();
            _disposed = true;
        }
    }
    #endregion
}
