using Application.Abstractions.Products;
using Application.Dtos.Produts;
using Application.FileServices;
using Application.Services.Contracts.Products;
using Domain.Entities;
using Domain.Exceptions.Products;
using Mapster;
using System.Threading;

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
            //Product? productResule = await _context.Products.FirstOrDefaultAsync(x => x.SortNumber == request.SortNumber, cancellationToken);

            //if (productResule != null)
            //    return 0;

            var products =  _repository.GetAllAsync();
            Product productresult =  products.FirstOrDefault(x => x.SortNumber == createProductDto.SortNumber);
            if (productresult != null)
            {
                return null;
            }

            string filepage = await _fileService.UploadImageAsync(createProductDto.VideoPath);

            var product = new Product()
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
            //string filepage = await _fileService.UploadImageAsync(modificationProduct.Videopath);
            //var result = await _repository.GetByIdAsync(id);

            //result.Name = modificationProduct.Name;
            //result.Description = modificationProduct.Description;
            //result.SortNumber = modificationProduct.SortNumber;


            //var product = await _repository.UpdateAsync(result);

            //return product;

            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                throw new ProductNotFound();

            if (modificationProduct.Name != null)
            {
                product.Name = modificationProduct.Name;
            }
            if (modificationProduct.Description != null)
            {
                product.Description = modificationProduct.Description;
            }
            if (modificationProduct.SortNumber != 0)
            {
                product.SortNumber = modificationProduct.SortNumber;
            }
            if (modificationProduct.Videopath != null)
            {
                await _fileService.DeletFileAsync(product.VideoPath);
                product.VideoPath = await _fileService.UploadImageAsync(modificationProduct.Videopath);
            }

            await _repository.UpdateAsync(product);
            return product;
        }
    }
}