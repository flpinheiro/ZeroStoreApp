using Microsoft.EntityFrameworkCore;
using ZeroStoreApp.Domain.Commons;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Repositories;
using ZeroStoreApp.Infra.Extensions;

namespace ZeroStoreApp.Infra.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly ZeroStoreAppDbContext _context;

    public ProductRepository(ZeroStoreAppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Product?> AddAsync(Product product, CancellationToken cancellationToken)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Product?> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(id, cancellationToken);
        if (product is null) return null;
        product.Delete();
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<PaginatedResult<Product>> GetPaginatedAsync(PaginatedQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.Query))
        {
            query = query.Where(p => p.Name.Contains(request.Query));
        }
        return await query.ToPaginatedAsync(request.Page, request.PageSize);
    }

    public async Task<Product?> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        product.Update();
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }
}
