using CustomerApiConsoleApp.Application.Interfaces;
using CustomerApiConsoleApp.Domain.Models;
using CustomerApiConsoleApp.Presentation;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApiConsoleApp.Test
{
    [TestClass]
    public class CustomerProcessorTests
    {
        [TestMethod]
        public async Task ProcessCustomersAsync_ShouldAddCustomersAndSaveChanges()
        {
            // Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var mockLogger = new Mock<ILogger<CustomerProcessor>>();

            var customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com" },
                new Customer { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com" }
            };

            mockCustomerService.Setup(s => s.GetCustomersAsync()).ReturnsAsync(customers);

            var customerProcessor = new CustomerProcessor(
                mockCustomerService.Object,
                mockCustomerRepository.Object,
                mockLogger.Object);

            // Act
            await customerProcessor.ProcessCustomersAsync();

            // Assert
            mockCustomerService.Verify(s => s.GetCustomersAsync(), Times.Once);
            mockCustomerRepository.Verify(r => r.AddCustomerAsync(It.IsAny<Customer>()), Times.Exactly(2));
            mockCustomerRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        public async Task ProcessCustomersAsync_WithEmptyCustomerList_ShouldNotAddOrSaveChanges()
        {
            // Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var mockLogger = new Mock<ILogger<CustomerProcessor>>();

            mockCustomerService.Setup(s => s.GetCustomersAsync()).ReturnsAsync(new List<Customer>());

            var customerProcessor = new CustomerProcessor(
                mockCustomerService.Object,
                mockCustomerRepository.Object,
                mockLogger.Object);

            // Act
            await customerProcessor.ProcessCustomersAsync();

            // Assert
            mockCustomerService.Verify(s => s.GetCustomersAsync(), Times.Once);
            mockCustomerRepository.Verify(r => r.AddCustomerAsync(It.IsAny<Customer>()), Times.Never);
            mockCustomerRepository.Verify(r => r.SaveChangesAsync(), Times.Never);
            mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString().Contains("No customers to process")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
