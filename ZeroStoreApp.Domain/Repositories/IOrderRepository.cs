using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.Domain.Enities;

namespace ZeroStoreApp.Domain.Repositories;

public interface IOrderRepository : IBaseRepository<Order>, IQueryOrderRepository { }

public interface IQueryOrderRepository : IBaseQueryRepository<Order> { }
