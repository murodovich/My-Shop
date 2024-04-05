namespace Domain.Exceptions.Products
{
    public class ProductNotFound : NotFoundException
    {
        public ProductNotFound()
        {
            TitleMessage = "Product Not Found !";
        }
    }
}
