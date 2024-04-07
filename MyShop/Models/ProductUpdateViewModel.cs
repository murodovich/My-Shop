namespace MyShop.Models
{
    public class ProductUpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Videopath { get; set; }
        public int SortNumber { get; set; }
    }
}
