using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Requests;

namespace ZeroStoreApp.Domain.Repositories;

public interface IProductRepository : IBaseRepository<Product>, IQueryProductRepRepository
{
}

public interface IQueryProductRepRepository : IBaseQueryRepository<Product>
{
    Task<IEnumerable<Product>> GetManyByIdAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task<PaginatedList<Product>> GetPaginatedAsync(PaginatedProductRequest request, CancellationToken cancellationToken);
}
