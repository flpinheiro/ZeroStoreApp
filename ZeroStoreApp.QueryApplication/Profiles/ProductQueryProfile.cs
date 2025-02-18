using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.Domain.Commons;
using ZeroStoreApp.Domain.Requests;
using ZeroStoreApp.Domain.Responses;
using ZeroStoreApp.QueryApplication.Queries;

namespace ZeroStoreApp.QueryApplication.Profiles;

public class ProductQueryProfile : Profile
{
    public ProductQueryProfile()
    {
        //GetPaginatedProductsQuery->PaginatedProductRequest
        CreateMap<GetPaginatedProductsQuery, PaginatedProductRequest>();
    }
}

public class PaginateProfile : Profile
{
    public PaginateProfile()
    {

        //CreateMap(typeof(PaginatedResult<>), typeof(PaginatedResponse<>)).
        //    .ConstructUsing((src , res) => new PaginatedResponse(src.))
        //    .ForCtorParam("nameof(PaginatedResponse.Data)", opt => opt.MapFrom(src=> src. ));
    }
}
