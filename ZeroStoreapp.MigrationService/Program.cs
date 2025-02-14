using ZeroStoreapp.MigrationService;
using ZeroStoreApp.Infra;


var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddSqlServerDbContext<ZeroStoreAppDbContext>("sqldata");

var host = builder.Build();
host.Run();
