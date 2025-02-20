using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.Domain.Commons;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Requests;
using ZeroStoreApp.Domain.Responses;
using ZeroStoreApp.QueryApplication.Queries;

namespace ZeroStoreApp.QueryApplication.Profiles;

public class ProductQueryProfile : Profile
{
    public ProductQueryProfile()
    {
        CreateMap<GetPaginatedProductsQuery, PaginatedProductRequest>();
        CreateMap<Product, PaginatedProductResponse>();
        CreateMap<Product, ProductResponse>();
    }
}
