using AutoMapper;
using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.CommandApplication.Events;
using ZeroStoreApp.Domain.Enities;

namespace ZeroStoreApp.CommandApplication.Profiles;

public class ProductCommandProfile : Profile
{
    public ProductCommandProfile()
    {
        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
        CreateMap<Product, ProductResponse>();
        CreateMap<Product, CreateProductEvent>();
        CreateMap<Product, UpdateProductEvent>();
        CreateMap<Product, DeleteProductEvent>();
    }
}
