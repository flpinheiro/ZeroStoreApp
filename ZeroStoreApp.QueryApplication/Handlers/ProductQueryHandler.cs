using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.CrossCutting.Common;
using ZeroStoreApp.CrossCutting.Helpers;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Requests;
using ZeroStoreApp.Domain.Responses;
using ZeroStoreApp.Domain.Services;
using ZeroStoreApp.QueryApplication.Profiles;
using ZeroStoreApp.QueryApplication.Queries;

namespace ZeroStoreApp.QueryApplication.Handlers;

public class ProductQueryHandler :
    IRequestHandler<GetProductQuery, ProductResponse>,
    IRequestHandler<GetPaginatedProductsQuery, PaginatedList<PaginatedProductResponse>>
{
    private readonly IUnitOfQuery _uow;
    private readonly IMapper _mapper;

    public ProductQueryHandler(IUnitOfQuery uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _uow.Products.GetByIdAsync(request.Id, cancellationToken);
        var response = _mapper.Map<ProductResponse>(product);
        return response;
    }

    public async Task<PaginatedList<PaginatedProductResponse>> Handle(GetPaginatedProductsQuery request, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<PaginatedProductRequest>(request);
        var result = await _uow.Products.GetPaginatedAsync(dto, cancellationToken);

        var respsonse = _mapper.MapPaginatedList<Product, PaginatedProductResponse>(result);

        return respsonse;
    }
}
