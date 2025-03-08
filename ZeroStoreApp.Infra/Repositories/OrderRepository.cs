using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Repositories;

namespace ZeroStoreApp.Infra.Repositories;

public class OrderRepository(ZeroStoreAppDbContext context) : BaseRepository<Order>(context), IOrderRepository
{
}
