using AutoMapper;
using MediatR;
using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Responses;
using ZeroStoreApp.Domain.Services;

namespace ZeroStoreApp.CommandApplication.Handlres;

public class ProductCommandHandler :
    IRequestHandler<CreateProductCommand, ProductResponse?>,
    IRequestHandler<UpdateProductCommand, ProductResponse?>,
    IRequestHandler<DeleteProductCommand, ProductResponse?>
{
    private readonly IUnitOfWork uow;
    private readonly IMapper _mapper;
    public ProductCommandHandler(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        _mapper = mapper;
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="request">Product information to be created</param>
    /// <param name="cancellationToken">cancelation token</param>
    /// <returns>ProductResponse</returns>
    /// <remarks>
    /// <list type="bullet"> true if the product was created successfully </list>
    /// <list type="bullet"> false if the product was not created successfully </list>
    /// </remarks>
    public async Task<ProductResponse?> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);
        await uow.Products.AddAsync(product, cancellationToken);
        var response = _mapper.Map<ProductResponse>(product);
        return response;
    }

    public async Task<ProductResponse?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // var product = _mapper.Map<Product>(request);
        
        var product = await uow.Products.GetByIdAsync(request.Id, cancellationToken);

        await uow.Products.UpdateAsync(product, cancellationToken);
        var response = _mapper.Map<ProductResponse>(product);
        return response;
    }

    public async Task<ProductResponse?> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await uow.Products.DeleteAsync(request.Id, cancellationToken);
        var response = _mapper.Map<ProductResponse>(product);
        return response;
    }
}
