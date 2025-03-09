using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text.Json;
using ZeroStoreApp.CommandApplication.Events;

namespace ZeroStoreApp.CommandApplication.Handlers;

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
