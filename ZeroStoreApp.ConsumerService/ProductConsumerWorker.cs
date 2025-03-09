using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using ZeroStoreApp.ConsumerService.Models;

namespace ZeroStoreApp.ConsumerService;

public class ProductConsumerWorker : BackgroundService, IDisposable
{
    private readonly ILogger<ProductConsumerWorker> _logger;
    private readonly IModel _channel;
    private readonly IConnection _connection;
    public ProductConsumerWorker(ILogger<ProductConsumerWorker> logger, IConnection connection)
    {
        _logger = logger;
        _connection = connection;
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "product-queue",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += Consumer_Received;

        _channel.BasicConsume(queue: "product-queue", autoAck: true, consumer: consumer);
        await Task.CompletedTask;
    }

    private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
    {
        var body = e.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        _logger.LogInformation("Received message: {message}", message);

        var response = JsonConvert.DeserializeObject<ProductResponse>(message);
        if (response == null)
        {
            _logger.LogError("Error deserializing message");
            return;
        }
        _logger.LogInformation("Product Id: {Id}", response.Id);
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
