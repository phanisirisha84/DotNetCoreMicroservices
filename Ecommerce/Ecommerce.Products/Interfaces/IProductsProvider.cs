using Ecommerce.Api.ProductsCore.Models;

namespace Ecommerce.Api.ProductsCore.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool isSuccess, IEnumerable<Product> products, string errorMessage)> GetProductsAsync();
        Task<(bool isSuccess, Product product, string errorMessage)> GetProductAsync(int id);
    }
}
