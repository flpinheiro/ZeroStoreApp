using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.Domain.Commons;
using ZeroStoreApp.Domain.Enities;

namespace ZeroStoreApp.Domain.Repositories;



public interface IProductRepository :IBasecommandRepository<Product>, IQueryProductRepRepository
{
}

public interface IQueryProductRepRepository : IBaseQueryRepository<Product>
{
}
