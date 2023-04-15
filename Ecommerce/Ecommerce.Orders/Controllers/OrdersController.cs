using Ecommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProvider ordersProvider;

        public OrdersController(IOrderProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrder(int customerId)
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
