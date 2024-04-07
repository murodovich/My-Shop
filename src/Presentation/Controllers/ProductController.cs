using Application.Dtos.Produts;
using Application.Services.Contracts.Products;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateProductAsync([FromForm]CreateProductDto createProductDto)
        {
            var result = await _productService.AddAsync(createProductDto);

            return Ok(result);
        }
        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
        {
            var products = await _productService.GetAllAsync();

            return Ok(products);

        }

        [HttpGet]
        public async ValueTask<IActionResult> GetByIdProduct(int id)
        {
            var result = await _productService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpPut]
        public async ValueTask<IActionResult> UpdateProductAsync([FromForm]ModificationProductDto modificationProduct, int id)
        {
            var result = await _productService.UpdateAsync(modificationProduct, id);

            return Ok(result);
        }

        [HttpDelete]
        public async ValueTask<IActionResult> DeleteProductAsync(int id)
        {
            var result =  _productService?.DeleteAsync(id);

            return Ok(result);
        }

    }
}
