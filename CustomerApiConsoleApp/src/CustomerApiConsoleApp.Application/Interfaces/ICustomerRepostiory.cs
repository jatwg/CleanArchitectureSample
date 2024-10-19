using CustomerApiConsoleApp.Domain.Models;

namespace CustomerApiConsoleApp.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddCustomerAsync(Customer customer);
        Task SaveChangesAsync();
    }
}