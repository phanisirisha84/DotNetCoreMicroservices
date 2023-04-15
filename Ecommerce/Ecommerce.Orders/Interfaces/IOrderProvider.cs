using Ecommerce.Api.Orders.Models;

namespace Ecommerce.Api.Orders.Interfaces
{
    public interface IOrderProvider
    {
        Task<(bool isSuccess, IEnumerable<Order> orders, string errorMessage)> GetOrdersAsync(int customerId);
    }
}
