using AutoMapper;
using MediatR;
using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.CommandApplication.Events;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Responses;
using ZeroStoreApp.Domain.Services;

namespace ZeroStoreApp.CommandApplication.Handlers;

public class ProductCommandHandler :
    IRequestHandler<CreateProductCommand, ProductResponse?>,
    IRequestHandler<UpdateProductCommand, ProductResponse?>,
    IRequestHandler<DeleteProductCommand, ProductResponse?>,
    IDisposable
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;

    public ProductCommandHandler(IUnitOfWork uow, IPublisher publisher, IMapper mapper)
    {
        _unitOfWork = uow;
        _mapper = mapper;
        _publisher = publisher;
    }

    #region Dispose
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _unitOfWork.Dispose();
        }
    }
    #endregion

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="request">Product information to be created</param>
    /// <param name="cancellationToken">cancelation token</param>
    /// <returns>ProductResponse</returns>
    public async Task<ProductResponse?> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var product = _mapper.Map<Product>(request);

        product = await _unitOfWork.Products.AddAsync(product, cancellationToken);

        if (product == null || request == null) return null;

        var response = _mapper.Map<ProductResponse>(product);

        var @event = _mapper.Map<CreateProductEvent>(response);

        await _publisher.Publish(@event, cancellationToken);

        return response;
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="request">Product information to be created</param>
    /// <param name="cancellationToken">cancelation token</param>
    /// <returns><see cref="ProductResponse"/> </returns>
    public async Task<ProductResponse?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var product = await _unitOfWork.Products.GetByIdAsync(request.Id, cancellationToken);

        if (product == null || request == null) return null;

        product.Name = request.Name ?? string.Empty;
        product.Description = request.Description ?? string.Empty;
        product.Price = request.Price;
        product.Stock = request.Stock;

        await _unitOfWork.Products.UpdateAsync(product, cancellationToken);

        var response = _mapper.Map<ProductResponse>(product);

        var @event = _mapper.Map<UpdateProductEvent>(response);

        await _publisher.Publish(@event, cancellationToken);

        return response;
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="request">information of the product to be deleted</param>
    /// <param name="cancellationToken">cancelation token</param>
    /// <returns><see cref="ProductResponse"/></returns>
    public async Task<ProductResponse?> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var product = await _unitOfWork.Products.DeleteAsync(request.Id, cancellationToken);

        if (product == null || request == null) return null;

        var response = _mapper.Map<ProductResponse>(product);

        var @event = _mapper.Map<DeleteProductEvent>(response);

        await _publisher.Publish(@event, cancellationToken);

        return response;
    }
}
