using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Requests;

namespace ZeroStoreApp.Domain.Repositories;

public interface IOrderRepository : IBaseRepository<Order>, IQueryOrderRepository { }

public interface IQueryOrderRepository : IBaseQueryRepository<Order> 
{
    Task<PaginatedList<Order>> GetPaginatedAsync(PaginateOrderRequest request, CancellationToken cancellationToken);
}
