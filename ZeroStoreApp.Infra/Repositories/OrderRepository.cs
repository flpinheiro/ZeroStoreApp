using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Repositories;
using ZeroStoreApp.Domain.Requests;
using ZeroStoreApp.Infra.Extensions;

namespace ZeroStoreApp.Infra.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ZeroStoreAppDbContext context): base(context)
    {
        
    }
    public async Task<PaginatedList<Order>> GetPaginatedAsync(PaginateOrderRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _context.Orders.Where(x => !x.IsDeleted).ToPaginatedListAsync(request, cancellationToken);
    }
}
