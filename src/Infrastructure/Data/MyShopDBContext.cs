using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class MyShopDBContext : DbContext
    {
        public MyShopDBContext(DbContextOptions<MyShopDBContext> options)
            : base(options)
        { }

        public DbSet<Product> products {  get; set; }
    }
}
