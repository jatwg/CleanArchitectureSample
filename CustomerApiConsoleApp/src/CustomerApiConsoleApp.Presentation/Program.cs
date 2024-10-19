using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CustomerApiConsoleApp.Application.Interfaces;
using CustomerApiConsoleApp.Application.Services;
using CustomerApiConsoleApp.Domain.Models;
using CustomerApiConsoleApp.Infrastructure.Data;
using CustomerApiConsoleApp.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using CustomerApiConsoleApp.Presentation.Configuration;

namespace CustomerApiConsoleApp.Presentation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .Build();

            var serviceProvider = ServiceProviderConfiguration.ConfigureServices(configuration);

            var customerProcessor = new CustomerProcessor(
                serviceProvider.GetService<ICustomerService>(),
                serviceProvider.GetService<ICustomerRepository>());

            await customerProcessor.ProcessCustomersAsync();

        }
    }
}