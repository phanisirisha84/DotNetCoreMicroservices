using Ecommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;

        public CustomersController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await customersProvider.GetCustomersAsync();

            if (result.isSuccess)
            {
                return Ok(result.customers);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await customersProvider.GetCustomerAsync(id);

            if (result.isSuccess)
            {
                return Ok(result.customer);
            }
            return NotFound();
        }
    }
}
