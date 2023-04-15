using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Orders.Database
{
    public class OrdersDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public OrdersDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
