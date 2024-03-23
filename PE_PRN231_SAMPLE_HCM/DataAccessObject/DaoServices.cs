using BussinessObject.Models;
using DataAccessObject.Daos;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessObject
{
    public static class DaoServices
    {
        public static IServiceCollection AddDaosServices(this IServiceCollection services)
        {
            services.AddScoped<GenericDao<BranchDao>>();
            services.AddScoped<GenericDao<Category>>();
            services.AddScoped<GenericDao<SilverJewelry>>();

            return services;
        }
    }
}
