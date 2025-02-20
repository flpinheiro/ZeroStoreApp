using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.Domain.Commons;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Requests;

namespace ZeroStoreApp.Domain.Repositories;

public interface IProductRepository : IBaseRepository<Product>, IQueryProductRepRepository
{
}

public interface IQueryProductRepRepository : IBaseQueryRepository<Product>
{
    Task<PaginatedList<Product>> GetPaginatedAsync(PaginatedProductRequest request, CancellationToken cancellationToken);
}
