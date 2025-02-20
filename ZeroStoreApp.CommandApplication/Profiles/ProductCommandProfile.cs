using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.CommandApplication.Events;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Responses;

namespace ZeroStoreApp.CommandApplication.Profiles;

public class ProductCommandProfile : Profile
{
    public ProductCommandProfile()
    {
        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
        CreateMap<Product, ProductResponse>();
        CreateMap<ProductResponse, CreateProductEvent>();
        CreateMap<ProductResponse, UpdateProductEvent>();
        CreateMap<ProductResponse, DeleteProductEvent>();
    }
}
