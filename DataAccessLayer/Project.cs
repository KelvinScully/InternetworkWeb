using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            //services.AddScoped<Interface, Implementation>();
            return services;
        }
    }

}
