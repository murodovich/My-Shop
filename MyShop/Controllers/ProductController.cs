using Application.Dtos.Produts;
using Application.Services.Contracts.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Models;

namespace MyShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto createProductDto)
        {
            var result = await _productService.AddAsync(createProductDto);

            return View("Product", result);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int pg = 1)
        {
            var tasks = await _productService.GetAllAsync();
            const int pageSize = 8;
            if (pg < 1)
            {
                pg = 1;
            }

            int recsCount = tasks.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = tasks.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(data);
        }

        public async ValueTask<IActionResult> Deleted(int id)
        {
            var user = await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async ValueTask<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            
            var productdto = new ModificationProductDto()
            {
                Name = product.Name,
                Description = product.Description,
                SortNumber = product.SortNumber,
            };
            return View(productdto);

        }

        [HttpPost]
        public async ValueTask<IActionResult> Update(ModificationProductDto product, int id)
        {
              var newProduct = await _productService.UpdateAsync(product, id);
              return RedirectToAction(nameof(Index));   
        }


        public async Task<IActionResult> Search(string text)
        {
            var books = await _productService.Search(text);

            return View(nameof(Index), books);
        }
    }
}
