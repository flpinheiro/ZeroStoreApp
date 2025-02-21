using Ocelot.Middleware;
using ZeroStoreApp.ApiService;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddControllers();

//Add Ocelot services
builder.Services.AddOcelotService(builder.Configuration);
builder.Services.AddSwaggerService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerMiddlare();
}

await app.UseOcelot();

app.MapDefaultEndpoints();

app.MapControllers();

app.Run();
