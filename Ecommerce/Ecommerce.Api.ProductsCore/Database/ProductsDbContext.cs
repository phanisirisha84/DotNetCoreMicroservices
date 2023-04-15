using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.ProductsCore.Database
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductsDbContext(DbContextOptions options): base(options)
        {

        }
    }
}
