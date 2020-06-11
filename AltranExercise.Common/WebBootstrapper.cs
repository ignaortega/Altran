using Data;
using Microsoft.Extensions.DependencyInjection;
using Service;
using System;

namespace Common
{
    public static class WebBootstrapper
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            Register(services);
            return services;
        }

        static void Register(IServiceCollection services)
        {
            // Register application-specific services in the dependency injection framework
            //ServiceBootstrapper.Bootstrap(new ServiceRegistrar(services));
            services.AddScoped<IAService, AService>();
            DataBootstrapper.Bootstrap(services);

        }
    }
}
