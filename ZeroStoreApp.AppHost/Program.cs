var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var password = builder.AddParameter("password", "Chageme@123", secret: true);

var sqlServer = builder.AddSqlServer("sqlServer", password, port: 14330)
                .WithDataVolume()
                .WithLifetime(ContainerLifetime.Persistent)
                .AddDatabase("database");

var commandService = builder.AddProject<Projects.ZeroStoreApp_CommandService>("commandservice");

var apiService = builder.AddProject<Projects.ZeroStoreApp_ApiService>("apiservice")
    .WithReference(commandService)
    .WaitFor(commandService);

builder.AddProject<Projects.ZeroStoreApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.ZeroStoreApp_QueryService>("zerostoreapp-queryservice");

builder.Build().Run();
