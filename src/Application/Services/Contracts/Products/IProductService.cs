using Application.Dtos.Produts;
using Domain.Entities;

namespace Application.Services.Contracts.Products
{
    public interface IProductService
    {
        ValueTask<Product> AddAsync(CreateProductDto createProductDto);
        ValueTask<List<Product>> GetAllAsync();
        ValueTask<Product> GetByIdAsync(int id);
        ValueTask<Product> UpdateAsync(ModificationProductDto modificationProduct, int id);
        ValueTask<Product> DeleteAsync(int id);
        ValueTask<IQueryable<Product>> Search(string query);
    }
}
