using AltranExercise.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AltranExercise.Data.Infraestructure
{
    public static class DataBootstrapper
    {
        public static IServiceCollection BootstrapData(this IServiceCollection services)
        {
            Register(services);
            return services;
        }

        static void Register(IServiceCollection services)
        {
            //services.AddScoped<IClientsRepository, ClientsRepository>();
            services.AddHttpClient<IPoliciesRepository, PoliciesRepository>();

            services.AddHttpClient<IClientsRepository, ClientsRepository>();
        }
    }
}
