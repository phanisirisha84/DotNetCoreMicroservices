using AutoMapper;
using Ecommerce.Api.Customers.Database;
using Ecommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext customersDbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext customersDbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.customersDbContext = customersDbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!customersDbContext.Customers.Any())
            {
                customersDbContext.Customers.Add(new Customer { Id = 1, Name = "Name1" });
                customersDbContext.Customers.Add(new Customer { Id = 2, Name = "Name2" });
                customersDbContext.Customers.Add(new Customer { Id = 3, Name = "Name3" });
                customersDbContext.Customers.Add(new Customer { Id = 4, Name = "Name4" });

                customersDbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, Models.Customer customer, string errorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await customersDbContext.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
                if (customer != null)
                {
                    var result = mapper.Map<Database.Customer, Models.Customer>(customer);
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

        public async Task<(bool isSuccess, IEnumerable<Models.Customer> customers, string errorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await customersDbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Database.Customer>, IEnumerable<Models.Customer>>(customers);
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
