using Microsoft.EntityFrameworkCore;
using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Repositories;
using ZeroStoreApp.Domain.Requests;
using ZeroStoreApp.Infra.Extensions;

namespace ZeroStoreApp.Infra.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ZeroStoreAppDbContext context) : base(context)
    {
    }

    public async Task<PaginatedList<Product>> GetPaginatedAsync(PaginatedProductRequest request, CancellationToken cancellationToken)
    {
        var query = _context.Products
            .AsNoTracking()
            .Where(p => !p.IsDeleted)
            .AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.Query))
        {
            query = query
                .Where(p =>
                p.Name.ToLowerInvariant().Contains(request.Query.ToLowerInvariant()) ||
                p.Description.ToLowerInvariant().Contains(request.Query.ToLowerInvariant()));
        }
        return await query.ToPaginatedListAsync(request.Page, request.PageSize);
    }
}
