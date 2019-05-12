using huncho.Data.Models;
using huncho.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace huncho.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Product>, Repository<Product>>();
            services.AddTransient<IRepository<Order>, Repository<Order>>();
        }
    }
}
