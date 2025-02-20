using ZeroStoreApp.ConsumerService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
//builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<ProductConsumerWorker>();

builder.AddRabbitMQClient("messaging");

var host = builder.Build();
host.Run();
