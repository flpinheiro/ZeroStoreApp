using Microsoft.EntityFrameworkCore;
using ZeroStoreApp.Domain.Commons;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Repositories;
using ZeroStoreApp.Domain.Requests;
using ZeroStoreApp.Infra.Extensions;

namespace ZeroStoreApp.Infra.Repositories;

internal class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ZeroStoreAppDbContext context) : base(context)
    {
    }

    //public override async Task<Product?> AddAsync(Product product, CancellationToken cancellationToken)
    //{
    //    _context.Products.Add(product);
    //    await _context.SaveChangesAsync(cancellationToken);
    //    await _context.Database.BeginTransactionAsync(cancellationToken);
    //    return product;
    //}

    public override async Task<Product?> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await GetByIdAsync(id, cancellationToken);
        if (product is null) return null;
        product.Delete();
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    //public override async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    //{
    //    return await _context.Products.ToListAsync(cancellationToken);
    //}

    public override async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Products.Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<PaginatedResult<Product>> GetPaginatedAsync(PaginatedProductRequest request, CancellationToken cancellationToken)
    {
        var query = _context.Products.AsNoTracking().Where(p => !p.IsDeleted).AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.Query))
        {
            query = query.Where(p => p.Name.Contains(request.Query));
        }
        return await query.ToPaginatedAsync(request.Page, request.PageSize);
    }

    public override async Task<Product?> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        product.Update();
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }
}
