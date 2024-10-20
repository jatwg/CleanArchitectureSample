using CustomerApiConsoleApp.Domain.Models;

namespace CustomerApiConsoleApp.Application.Interfaces
{
    public interface ICustomerDataSource
    {
        Task<List<Customer>> FetchCustomersAsync();
    }
}
