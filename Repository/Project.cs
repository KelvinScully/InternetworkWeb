using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class RepositoryService
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<ITestRepository, TestRepository>();
            return services;
        }
    }
}
