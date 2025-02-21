using AutoMapper;
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
