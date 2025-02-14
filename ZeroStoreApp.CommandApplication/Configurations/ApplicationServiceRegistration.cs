using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ZeroStoreApp.CommandApplication.Configurations;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddCommandServices(this IServiceCollection services)
    {

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
