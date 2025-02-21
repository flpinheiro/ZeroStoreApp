using ZeroStoreApp.Domain.Enities;

namespace ZeroStoreApp.Domain.Repositories;

public interface IBaseRepository<TEntity> : IBaseQueryRepository<TEntity>, IBasecommandRepository<TEntity> where TEntity : BaseEntity
{
}

public interface IBaseQueryRepository<TEntity> : IDisposable where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}

public interface IBasecommandRepository<TEntity> : IDisposable where TEntity : BaseEntity
{
    Task<TEntity?> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity?> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
