using AltranExercise.Data.Infraestructure;
using AltranExercise.Service.Mapping;
using AltranExercise.Service.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AltranExercise.Service.Infraestructure
{
    public static class ServiceBoostrapper
    {
        public static IServiceCollection BootstrapServices(this IServiceCollection services)
        {
            Register(services);
            ConfigureAutomapper(services);
            return services;
        }

        private static void Register(IServiceCollection services)
        {
            // Register application-specific services in the dependency injection framework
            services.BootstrapData();

            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<IPoliciesService, PoliciesService>();
        }

        private static void ConfigureAutomapper(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
