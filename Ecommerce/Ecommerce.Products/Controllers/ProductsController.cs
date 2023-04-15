using Ecommerce.Api.ProductsCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.ProductsCore.Controllers
{
    [ApiController]
    [Route("api/Products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await productsProvider.GetProductsAsync();

            if (result.isSuccess)
            {
                return Ok(result.products);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await productsProvider.GetProductAsync(id);

            if (result.isSuccess)
            {
                return Ok(result.product);
            }
            return NotFound();
        }
    }
}
