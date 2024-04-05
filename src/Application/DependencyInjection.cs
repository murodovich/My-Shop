using Application.FileServices;
using Application.Services.Contracts.Products;
using Application.Services.Products;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
