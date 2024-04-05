using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Produts
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile VideoPath { get; set; }
        public int SortNumber { get; set; }
    }
}
