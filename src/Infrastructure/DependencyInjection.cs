using Application.Abstractions.Products;
using Infrastructure.Data;
using Infrastructure.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<MyShopDBContext>(options =>
                options
                .UseSqlServer(configuration.GetConnectionString("SqlServer")));

            services.AddScoped<IProductRepository, ProductRepository>();
            
            
            return services;
        }

    }
}
