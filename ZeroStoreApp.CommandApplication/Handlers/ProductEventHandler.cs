using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using ZeroStoreApp.CommandApplication.Events;

namespace ZeroStoreApp.CommandApplication.Handlers;

public class ProductEventHandler : INotificationHandler<CreateProductEvent>,
    INotificationHandler<UpdateProductEvent>,
    INotificationHandler<DeleteProductEvent>,
    IDisposable
{
    private readonly ILogger<ProductEventHandler> _logger;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    public ProductEventHandler(ILogger<ProductEventHandler> logger, IConnection connection)
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

    public async Task Handle(CreateProductEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create Product Event: {Id}", notification.Id);

        cancellationToken.ThrowIfCancellationRequested();

        _channel.QueueDeclare(queue: "product-queue",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

        _channel.BasicPublish(exchange: "",
             routingKey: "product-queue",
             basicProperties: null,
             body: notification.ToBytes());

        await Task.CompletedTask;
    }

    public async Task Handle(UpdateProductEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Update Product Event: {Id}", notification.Id);

        cancellationToken.ThrowIfCancellationRequested();

        _channel.QueueDeclare(queue: "product-queue",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

        _channel.BasicPublish(exchange: "",
             routingKey: "product-queue",
             basicProperties: null,
             body: notification.ToBytes());

        await Task.CompletedTask;
    }

    public async Task Handle(DeleteProductEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete Product Event: {Id}", notification.Id);

        cancellationToken.ThrowIfCancellationRequested();

        _channel.QueueDeclare(queue: "product-queue",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

        _channel.BasicPublish(exchange: "",
             routingKey: "product-queue",
             basicProperties: null,
             body: notification.ToBytes());

        await Task.CompletedTask;
    }
}