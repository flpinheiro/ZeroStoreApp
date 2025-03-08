using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroStoreApp.CommandApplication.Commands;
using ZeroStoreApp.CommandApplication.Events;
using ZeroStoreApp.Domain.Enities;
using ZeroStoreApp.Domain.Services;
using ZeroStoreApp.Domain.ValueObjects;
using System.Text.Json;

namespace ZeroStoreApp.CommandApplication.Handlers;

public class OrderCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher) : IRequestHandler<CreateOrderCommand>
{

    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order();

        var productIds = request.Items.Select(x => x.ProductId).ToList();
        var products = await unitOfWork.Products.GetManyByIdAsync(productIds, cancellationToken);

        foreach (var product in products)
        {
            var item = request.Items.FirstOrDefault(a => a.ProductId == product.Id);
            if (item is null) continue;
            var orderItem = new OrderItem(product, item.Quantity)
            {
                Order = order
            };
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

internal class OrderEventHandler : INotificationHandler<CreateOrderEvent>, IDisposable
{
    private readonly ILogger<CreateOrderEvent> _logger;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public OrderEventHandler(ILogger<CreateOrderEvent> logger, IConnection connection)
    {
        _logger = logger;
        _connection = connection;
        _channel = _connection.CreateModel();
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
            _channel.Dispose();
        }
    }
    #endregion
    public async Task Handle(CreateOrderEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create Order Event");

        cancellationToken.ThrowIfCancellationRequested();

        _channel.QueueDeclare(queue: "order-queue",
             durable: false,
             exclusive: false,
             autoDelete: false,
             arguments: null);

        var @bytes = JsonSerializer.SerializeToUtf8Bytes(notification);

        _channel.BasicPublish(exchange: "",
             routingKey: "order-queue",
             basicProperties: null,
             body: @bytes);

        await Task.CompletedTask;
    }
}
