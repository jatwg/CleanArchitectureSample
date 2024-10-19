using CustomerApiConsoleApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerApiConsoleApp.Infrastructure.Data
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }
    }
}