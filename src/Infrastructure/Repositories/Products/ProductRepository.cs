using Application.Abstractions.Products;
using Domain.Entities;
using Domain.Exceptions.Products;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyShopDBContext _shopDBContext;

        public ProductRepository(MyShopDBContext shopDBContext)
            => _shopDBContext = shopDBContext;

        public async ValueTask<Product> CreateAsync(Product entity)
        {
            var entry = await _shopDBContext.products.AddAsync(entity);
            await _shopDBContext.SaveChangesAsync();

            return entry.Entity;
        }

        public async ValueTask<Product> DeleteAsync(int Id)
        {
            var product = await _shopDBContext.products.FirstOrDefaultAsync(x => x.Id == Id);
            if (product == null)
            throw new ProductNotFound();

            _shopDBContext.products.Remove(product);
            await _shopDBContext.SaveChangesAsync();

            return product;
        }

        public IQueryable<Product> GetAllAsync()
        {
            var users = _shopDBContext.products.AsNoTracking();

            return users;
        }

        public async ValueTask<Product> GetByIdAsync(int id)
        {
            var product = await _shopDBContext.products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                throw new ProductNotFound();

            return product;
        }

        public async ValueTask<Product> UpdateAsync(Product entity)
        {
            var product = await _shopDBContext.products.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (product == null)
                throw new ProductNotFound();

            return product;
        }
    }
}
