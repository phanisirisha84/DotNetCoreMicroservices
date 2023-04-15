using AutoMapper;
using Ecommerce.Api.ProductsCore.Database;
using Ecommerce.Api.ProductsCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.ProductsCore.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext productsDbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext productsDbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.productsDbContext = productsDbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!productsDbContext.Products.Any())
            {
                productsDbContext.Products.Add(new Product{ Id = 1, Name = "Keyboard", Price = 100, Inventory = 1});
                productsDbContext.Products.Add(new Product{ Id = 2, Name = "CPU", Price = 200, Inventory = 2});
                productsDbContext.Products.Add(new Product{ Id = 3, Name = "Mouse", Price = 300, Inventory = 3});
                productsDbContext.Products.Add(new Product{ Id = 4, Name = "Monitor", Price = 400, Inventory = 4});

                productsDbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Product> products, string errorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await productsDbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Database.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, Models.Product product, string errorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await productsDbContext.Products.Where(e => e.Id == id).FirstOrDefaultAsync();
                if (product != null)
                {
                    var result = mapper.Map<Database.Product, Models.Product>(product);
                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
