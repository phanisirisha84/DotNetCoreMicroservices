using AutoMapper;
using Ecommerce.Api.Orders.Database;
using Ecommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrderProvider
    {
        private readonly OrdersDbContext productsDbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext productsDbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.productsDbContext = productsDbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!productsDbContext.Orders.Any())
            {
                productsDbContext.Orders.Add(new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                productsDbContext.Orders.Add(new Order()
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(-1),
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                productsDbContext.Orders.Add(new Order()
                {
                    Id = 3,
                    CustomerId = 2,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                productsDbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Order> orders, string errorMessage)> GetOrdersAsync(int customerId)
        {
            IEnumerable<Order> orders = await productsDbContext.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
            if (orders != null && orders.Any())
            {
                var result = mapper.Map<IEnumerable<Database.Order>, IEnumerable<Models.Order>>(orders);
                return (true, result, null);
            }

            return (false, null, "Not found");
        }
    }
}
