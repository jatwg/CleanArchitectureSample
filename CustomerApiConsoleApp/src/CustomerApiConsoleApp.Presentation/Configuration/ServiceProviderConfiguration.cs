using CustomerApiConsoleApp.Application.Interfaces;
using CustomerApiConsoleApp.Application.Services;
using CustomerApiConsoleApp.Infrastructure.Data;
using CustomerApiConsoleApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiConsoleApp.Presentation.Configuration
{
    public static class ServiceProviderConfiguration
    {
        public static ServiceProvider ConfigureServices(IConfiguration configuration)
        {
            return new ServiceCollection()
                .AddDbContext<CustomerDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CustomerDb")))
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<ICustomerService, CustomerService>()
                .AddSingleton<IConfiguration>(configuration)
                .AddHttpClient()
                .BuildServiceProvider();
        }
    }
}
