using Microsoft.EntityFrameworkCore;
using CustomerApiConsoleApp.Domain.Models;
using System.Collections.Generic;

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