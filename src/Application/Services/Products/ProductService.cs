using Application.Abstractions.Products;
using Application.Dtos.Produts;
using Application.FileServices;
using Application.Services.Contracts.Products;
using Domain.Entities;
using Mapster;

namespace Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IFileService _fileService;
        public ProductService(IProductRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async ValueTask<Product> AddAsync(CreateProductDto createProductDto)
        {
            string filepage = await _fileService.UploadImageAsync(createProductDto.VideoPath);

            var product =new  Product()
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                VideoPath = filepage,
                SortNumber = createProductDto.SortNumber,
            };
            var result = await _repository.CreateAsync(product);

            return result;
        }

        public async ValueTask<Product> DeleteAsync(int id)
        {
            var product = await _repository.DeleteAsync(id);

            return product;
        }

        public async ValueTask<List<Product>> GetAllAsync()
        {
            var products = _repository.GetAllAsync().ToList();

            return products;
        }

        public async ValueTask<Product> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);  

            return product;
        }

        public async ValueTask<IQueryable<Product>> Search(string query)
        {
            var result = await _repository.Search(query);
            return result;
        }

        public async ValueTask<Product> UpdateAsync(ModificationProductDto modificationProduct, int id)
        {
            var result = modificationProduct.Adapt<Product>();
            result.Id = id;

            var product = await _repository.UpdateAsync(result);

            return product;
        }
    }
}
