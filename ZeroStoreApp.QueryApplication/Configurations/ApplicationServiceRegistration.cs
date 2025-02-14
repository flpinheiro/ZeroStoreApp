﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ZeroStoreApp.QueryApplication.Configurations;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddQueryServices(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
