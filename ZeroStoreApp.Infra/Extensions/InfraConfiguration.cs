using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZeroStoreApp.Domain.Repositories;
using ZeroStoreApp.Infra.Repositories;

namespace ZeroStoreApp.Infra.Extensions;

public static class InfraConfiguration
{
    public static IServiceCollection AddInfraConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ZeroStoreAppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                action => action.MigrationsAssembly("ZeroStoreApp.Infra"));
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IQueryProductRepRepository, ProductRepository>();

#if DEBUG
        using var scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ZeroStoreAppDbContext>();
        context.Database.Migrate();
#endif

        return services;
    }
}
