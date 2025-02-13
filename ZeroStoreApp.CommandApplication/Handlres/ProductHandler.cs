using AutoMapper;
using MediatR;
using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.CommandApplication.Responses;
using ZeroStoreApp.CrossCutting.Constants;
using ZeroStoreApp.CrossCutting.Exceptions;
using ZeroStoreApp.Domain.Commons;
using ZeroStoreApp.Domain.Repositories;
using ZeroStoreApp.Domain.Services;

namespace ZeroStoreApp.CommandApplication.Handlres;

public class ProductHandler :
    IRequestHandler<CreateProductCommand, bool>,
    IRequestHandler<UpdateProductCommand, bool>,
    IRequestHandler<DeleteProductCommand, bool>
    //IRequestHandler<GetProductCommand, ProductResponse>,
    //IRequestHandler<GetProductsCommand, PaginatedResponse<PaginatedProductResponse>>
{
    private readonly IUnitOfWork uow;
    private readonly IMapper _mapper;
    public ProductHandler(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        _mapper = mapper;
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="request">Product information to be created</param>
    /// <param name="cancellationToken">cancelation token</param>
    /// <returns>bool</returns>
    /// <remarks>
    /// <list type="bullet"> true if the product was created successfully </list>
    /// <list type="bullet"> false if the product was not created successfully </list>
    /// </remarks>
    public Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    //public async Task<ProductResponse> Handle(GetProductCommand request, CancellationToken cancellationToken)
    //{
    //    var product = await uow.Products.GetByIdAsync(request.Id, cancellationToken);

    //    if (product == null)
    //    {
    //        throw new NotFoundException(ResponseMessages.ProductNotFound);
    //    }
    //    var response = _mapper.Map<ProductResponse>(product);
    //    return response;
    //}

    //public async Task<PaginatedResponse<PaginatedProductResponse>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
    //{
    //    var query = _mapper.Map<PaginatedQuery>(request);
    //    var products = await uow.Products.GetPaginatedAsync(query, cancellationToken);
    //    throw new NotImplementedException();
    //}
}
