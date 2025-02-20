var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

//var username = builder.AddParameter("username", "admin", secret: true);

var password = builder.AddParameter("password", "Chageme@123", secret: true);

var rabbitmq = builder.AddRabbitMQ("messaging");

var sql = builder.AddSqlServer("sql", password: password, port: 14330)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("sqldata");

var migration = builder.AddProject<Projects.ZeroStoreapp_MigrationService>("migrations")
    .WithReference(sql)
    .WaitFor(sql);

var commandService = builder.AddProject<Projects.ZeroStoreApp_CommandService>("commandservice")
    .WaitFor(migration)
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    .WithReference(sql)
    .WaitFor(sql);

var queryService = builder.AddProject<Projects.ZeroStoreApp_QueryService>("queryservice")
    .WaitFor(migration)
    .WithReference(sql)
    .WaitFor(sql);

var apiService = builder.AddProject<Projects.ZeroStoreApp_ApiService>("apiservice")
    .WithReference(queryService)
    .WaitFor(queryService)
    .WithReference(commandService)
    .WaitFor(commandService);

builder.AddProject<Projects.ZeroStoreApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.ZeroStoreApp_ConsumerService>("zerostoreapp-consumerservice")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

builder.Build().Run();
