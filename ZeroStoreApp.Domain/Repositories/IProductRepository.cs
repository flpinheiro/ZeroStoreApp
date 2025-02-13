using ZeroStoreApp.Domain.Commons;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Queries;

namespace ZeroStoreApp.Domain.Repositories;



public interface IProductRepository : IBaseRepository<Product>, IQueryProductRepRepository
{
}

public interface IQueryProductRepRepository : IBaseQueryRepository<Product>
{
    Task<PaginatedResult<Product>> GetPaginatedAsync(PaginatedProductQuery request, CancellationToken cancellationToken);
}
