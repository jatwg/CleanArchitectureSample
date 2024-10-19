using System.Threading.Tasks;
using CustomerApiConsoleApp.Application.Interfaces;
using CustomerApiConsoleApp.Domain.Models;
using CustomerApiConsoleApp.Infrastructure.Data;

namespace CustomerApiConsoleApp.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _dbContext;

        public CustomerRepository(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}