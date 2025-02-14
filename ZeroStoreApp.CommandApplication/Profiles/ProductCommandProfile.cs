using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Responses;

namespace ZeroStoreApp.CommandApplication.Profiles;

public class ProductCommandProfile : Profile
{
    public ProductCommandProfile()
    {
        //CreateProductCommand -> Product
        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
        //Product -> ProductResponse
        CreateMap<Product, ProductResponse>();
    }
}
