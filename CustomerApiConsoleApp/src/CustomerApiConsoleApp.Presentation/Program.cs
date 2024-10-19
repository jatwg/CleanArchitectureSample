using CustomerApiConsoleApp.Application.Interfaces;
using CustomerApiConsoleApp.Presentation.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;

namespace CustomerApiConsoleApp.Presentation
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        static async Task Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .Build();
            var logger = LogManager.GetCurrentClassLogger();

            var serviceProvider = ServiceProviderConfiguration.ConfigureServices(configuration);

            var customerProcessor = new CustomerProcessor(
                serviceProvider.GetService<ICustomerService>(),
                serviceProvider.GetService<ICustomerRepository>(),
                serviceProvider.GetService<ILogger<CustomerProcessor>>());

            try
            {
                logger.Debug("Started...");
                await customerProcessor.ProcessCustomersAsync();
                logger.Debug("Completed...");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "An error occurred in the main program.");
            }
            finally
            {
                LogManager.Shutdown();
            }



        }
    }
}