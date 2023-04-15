using Ecommerce.Api.Customers.Models;

namespace Ecommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool isSuccess, IEnumerable<Customer> customers, string errorMessage)> GetCustomersAsync();
        Task<(bool isSuccess, Customer customer, string errorMessage)> GetCustomerAsync(int id);
    }
}
