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

    }
}
