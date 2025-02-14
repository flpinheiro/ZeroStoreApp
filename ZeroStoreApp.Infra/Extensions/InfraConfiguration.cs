using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZeroStoreApp.Domain.Repositories;
using ZeroStoreApp.Domain.Services;
using ZeroStoreApp.Infra.Repositories;
using ZeroStoreApp.Infra.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace ZeroStoreApp.Infra.Extensions;

public static class InfraConfiguration
{
    public static IServiceCollection AddInfraConfiguration(this WebApplicationBuilder builder) 
    {
        builder.AddSqlServerDbContext<ZeroStoreAppDbContext>(connectionName: "database");

        builder.Services.AddInfraConfiguration(builder.Configuration);

        return builder.Services;
    }
    private static IServiceCollection AddInfraConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<ZeroStoreAppDbContext>(options =>
        //{
        //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        //        action => action.MigrationsAssembly("ZeroStoreApp.Infra"));
        //});

        // add unit of work service
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUnitOfQuery, UnitOfQuery>();

        // add repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IQueryProductRepRepository, ProductRepository>();

        // add lazy services
        services.AddScoped(provider => new Lazy<IProductRepository>(() => provider.GetRequiredService<IProductRepository>()));
        services.AddScoped(provider => new Lazy<IQueryProductRepRepository>(() => provider.GetRequiredService<IQueryProductRepRepository>()));

//#if DEBUG
//        using var scope = services.BuildServiceProvider().CreateScope();
//        var context = scope.ServiceProvider.GetRequiredService<ZeroStoreAppDbContext>();
//        context.Database.Migrate();
//#endif

        return services;
    }
}


