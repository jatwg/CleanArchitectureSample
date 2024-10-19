using CustomerApiConsoleApp.Domain.Models;

namespace CustomerApiConsoleApp.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomersAsync();
    }
}