using MediatR;
using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.CommandApplication.Events;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Services;
using ZeroStoreApp.Domain.ValueObjects;

namespace ZeroStoreApp.CommandApplication.Handlers;

public class OrderCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher) : IRequestHandler<CreateOrderCommand>,
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

    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order();

        var productIds = request.Items.Select(x => x.ProductId).ToList();
        var products = await unitOfWork.Products.GetManyByIdAsync(productIds, cancellationToken);

        foreach (var product in products)
        {
            var item = request.Items.FirstOrDefault(a => a.ProductId == product.Id);
            if (item is null) continue;
            var orderItem = new OrderItem(product, order, item.Quantity);
            order.AddItem(orderItem);
        }

        await unitOfWork.Orders.AddAsync(order, cancellationToken);

        var @event = new CreateOrderEvent()
        {
            OrderId = order.Id
        };

        await publisher.Publish(@event, cancellationToken);
    }
}
