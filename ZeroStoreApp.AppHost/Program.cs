var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var commandService =  builder.AddProject<Projects.ZeroStoreApp_CommandService>("commandservice");

var apiService = builder.AddProject<Projects.ZeroStoreApp_ApiService>("apiservice")
    .WithReference(commandService)
    .WaitFor(commandService);

builder.AddProject<Projects.ZeroStoreApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
