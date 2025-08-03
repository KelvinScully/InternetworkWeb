using DataAccessLayer.Interfaces;
using DataAccessLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            //services.AddScoped<Interface, Implementation>();
            services.AddScoped<IInventoryDalService, InventoryDalService>();
            return services;
        }
    }

}
