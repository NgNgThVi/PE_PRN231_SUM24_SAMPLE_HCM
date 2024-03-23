using Microsoft.Extensions.DependencyInjection;
using Repository.IRepo;

namespace Repository.Repo
{
    public static class RepositoryServices
    {
        public static IServiceCollection AddRepositorySerivce(this IServiceCollection services)
        {
            services.AddScoped<IBranchAccountRepository, BranchAccountRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISilverJewelryRepository, SilverJewelryRepository>();
            return services;    
        }
    }
}
