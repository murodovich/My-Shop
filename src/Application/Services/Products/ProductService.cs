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

        public async ValueTask<Product> AddAsync(CreateProductDto userCreationDTO)
        {
            string filepage = await _fileService.UploadVideoAsync(userCreationDTO.VideoPath);

            var product =new  Product()
            {
                Name = userCreationDTO.Name,
                Description = userCreationDTO.Description,
                VideoPath = filepage,
                SortNumber = userCreationDTO.SortNumber,
            };
            var result = await _repository.CreateAsync(product);

            return result;
        }

        public ValueTask<Product> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<List<Product>> GetAllAsync()
        {
            var products = _repository.GetAllAsync().ToList();

            return products;
        }

        public ValueTask<Product> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Product> UpdateAsync(ModificationProductDto userModificationDTO, long id)
        {
            throw new NotImplementedException();
        }
    }
}
