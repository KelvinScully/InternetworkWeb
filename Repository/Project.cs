using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Repository.Services;

namespace Repository
{
    public static class RepositoryService
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            //services.AddScoped<Interface, Implementation>();
            services.AddScoped<IInventoryRepoService, InventoryRepoServices>();
            return services;
        }
    }
}
