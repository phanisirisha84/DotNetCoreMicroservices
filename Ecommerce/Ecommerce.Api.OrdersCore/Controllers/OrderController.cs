using Ecommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProvider ordersProvider;

        public OrderController(IOrderProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetProduct(int customerId)
        {
            var result = await ordersProvider.GetOrdersAsync(customerId);

            if (result.isSuccess)
            {
                return Ok(result.orders);
            }
            return NotFound();
        }
    }
}
