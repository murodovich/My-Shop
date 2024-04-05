using Application.Dtos.Produts;
using Domain.Entities;

namespace Application.Services.Contracts.Products
{
    public interface IProductService
    {
        ValueTask<Product> AddAsync(CreateProductDto userCreationDTO);
        ValueTask<List<Product>> GetAllAsync();
        ValueTask<Product> GetByIdAsync(long id);
        ValueTask<Product> UpdateAsync(ModificationProductDto userModificationDTO, long id);
        ValueTask<Product> DeleteAsync(long id);
    }
}
