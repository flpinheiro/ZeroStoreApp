using ZeroStoreApp.Domain.Enities;

namespace ZeroStoreApp.Domain.Repositories;

public interface IBaseRepository<TEntity> : IBaseQueryRepository<TEntity>, IBasecommandRepository<TEntity> where TEntity : BaseEntity
{
}

public interface IBaseQueryRepository<TEntity> : IDisposable where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}

public interface IBasecommandRepository<TEntity> : IDisposable where TEntity : BaseEntity
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
