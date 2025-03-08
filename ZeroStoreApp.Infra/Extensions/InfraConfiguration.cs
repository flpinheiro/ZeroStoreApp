using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZeroStoreApp.Domain.Repositories;
using ZeroStoreApp.Domain.Services;
using ZeroStoreApp.Infra.Repositories;
using ZeroStoreApp.Infra.Services;

namespace ZeroStoreApp.Infra.Extensions;

public static class InfraConfiguration
{
    public static IServiceCollection AddInfraConfiguration(this WebApplicationBuilder builder)
    {
        builder.AddSqlServerDbContext<ZeroStoreAppDbContext>(connectionName: "sqldata");

        builder.Services.AddInfraConfiguration(builder.Configuration);

        return builder.Services;
    }
    private static IServiceCollection AddInfraConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // add unit of work service
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUnitOfQuery, UnitOfQuery>();

        // add repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IQueryProductRepRepository, ProductRepository>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IQueryOrderRepository, OrderRepository>();

        // add lazy services
        services.AddScopedLazy<IProductRepository>();
        services.AddScopedLazy<IQueryProductRepRepository>();

        services.AddScopedLazy<IOrderRepository>();
        services.AddScopedLazy<IQueryOrderRepository>();

        return services;
    }

    private static void AddScopedLazy<TInterface>(this IServiceCollection services) where TInterface : class
    {
        services.AddScoped(provider => new Lazy<TInterface>(() => provider.GetRequiredService<TInterface>()));
    }
}


