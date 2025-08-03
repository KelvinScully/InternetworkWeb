using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer
{
    public static class BusinessLogicServiceRegistration
    {
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        {
            //services.AddScoped<Interface, Implementation>();
            services.AddScoped<IInventoryBllService, InventoryBllService>();
            return services;
        }
    }

}
