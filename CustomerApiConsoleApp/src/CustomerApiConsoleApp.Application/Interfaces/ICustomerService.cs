using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerApiConsoleApp.Domain.Models;

namespace CustomerApiConsoleApp.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomersAsync();
    }
}