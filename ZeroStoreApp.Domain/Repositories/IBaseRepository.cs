using ZeroStoreApp.Domain.Commons;

namespace ZeroStoreApp.Domain.Repositories;

public interface IBaseRepository<TEntity> : IBaseQueryRepository<TEntity>, IBasecommandRepository<TEntity> where TEntity : BaseEntity
{
}

public interface IBaseQueryRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PaginatedResult<TEntity>> GetPaginatedAsync(PaginatedQuery request, CancellationToken cancellationToken);
}

public interface IBasecommandRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> AddAsync(TEntity product, CancellationToken cancellationToken);
    Task<TEntity?> UpdateAsync(TEntity product, CancellationToken cancellationToken);
    Task<TEntity?> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
