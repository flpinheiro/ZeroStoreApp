using ZeroStoreApp.Domain.Commons;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Requests;

namespace ZeroStoreApp.Domain.Repositories;



public interface IProductRepository : IBaseRepository<Product>, IQueryProductRepRepository
{
}

public interface IQueryProductRepRepository : IBaseQueryRepository<Product>
{
    Task<PaginatedResult<Product>> GetPaginatedAsync(PaginatedProductRequest request, CancellationToken cancellationToken);
}
