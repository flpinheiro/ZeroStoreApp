using AutoMapper;
using MediatR;
using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.CommandApplication.Events;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Services;

namespace ZeroStoreApp.CommandApplication.Handlers;

public class ProductCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher, IMapper mapper) :
    IRequestHandler<CreateProductCommand, Guid?>,
    IRequestHandler<UpdateProductCommand, Guid?>,
    IRequestHandler<DeleteProductCommand, Guid?>,
    IDisposable
{
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
            unitOfWork.Dispose();
        }
    }
    #endregion

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="request">Product information to be created</param>
    /// <param name="cancellationToken">cancelation token</param>
    /// <returns><see cref="Guid"/> </returns>
    public async Task<Guid?> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var product = mapper.Map<Product>(request);

        await unitOfWork.Products.AddAsync(product, cancellationToken);

        var @event = mapper.Map<CreateProductEvent>(product);

        await publisher.Publish(@event, cancellationToken);

        return product.Id;
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="request">Product information to be created</param>
    /// <param name="cancellationToken">cancelation token</param>
    /// <returns><see cref="Guid"/> </returns>
    public async Task<Guid?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var product = await unitOfWork.Products.GetByIdAsync(request.Id, cancellationToken);

        if (product == null) return null;

        product.Update(product);

        await unitOfWork.Products.UpdateAsync(product, cancellationToken);

        var @event = mapper.Map<UpdateProductEvent>(product);

        await publisher.Publish(@event, cancellationToken);

        return product.Id;
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="request">information of the product to be deleted</param>
    /// <param name="cancellationToken">cancelation token</param>
    /// <returns><see cref="Guid"/></returns>
    public async Task<Guid?> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var product = await unitOfWork.Products.GetByIdAsync(request.Id, cancellationToken);

        if (product == null) return null;

        await unitOfWork.Products.DeleteAsync(request.Id, cancellationToken);

        var @event = mapper.Map<DeleteProductEvent>(product);

        await publisher.Publish(@event, cancellationToken);

        return product.Id;
    }
}
