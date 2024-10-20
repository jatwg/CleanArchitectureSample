using CustomerApiConsoleApp.Application.Dto;
using CustomerApiConsoleApp.Application.Services;
using CustomerApiConsoleApp.Domain.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace CustomerApiConsoleApp.Test
{
    [TestClass]
    public class CustomerServiceTests
    {
        [TestMethod]
        public async Task GetCustomersAsync_ShouldReturnListOfCustomers()
        {
            // Arrange
            var customerDtos = new List<CustomerDto>
            {
                new CustomerDto { id = 1, first_name = "John", last_name = "Doe", email = "john@example.com" },
                new CustomerDto { id = 2, first_name = "Jane", last_name = "Smith", email = "jane@example.com" }
            };

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(customerDtos))
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(c => c["ApiSettings:CustomerApiUrl"]).Returns("http://test-api.com/customers");

            var customerService = new CustomerService(httpClient, mockConfiguration.Object);

            // Act
            var result = await customerService.GetCustomersAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("John", result[0].FirstName);
            Assert.AreEqual("Doe", result[0].LastName);
            Assert.AreEqual("john@example.com", result[0].Email);
            Assert.AreEqual("Jane", result[1].FirstName);
            Assert.AreEqual("Smith", result[1].LastName);
            Assert.AreEqual("jane@example.com", result[1].Email);
        }

        [TestMethod]
        public async Task GetCustomersAsync_WhenApiReturnsEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[]")
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(c => c["ApiSettings:CustomerApiUrl"]).Returns("http://test-api.com/customers");

            var customerService = new CustomerService(httpClient, mockConfiguration.Object);

            // Act
            var result = await customerService.GetCustomersAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetCustomersAsync_WhenApiReturnsError_ShouldThrowException()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Internal Server Error")
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(c => c["ApiSettings:CustomerApiUrl"]).Returns("http://test-api.com/customers");

            var customerService = new CustomerService(httpClient, mockConfiguration.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => customerService.GetCustomersAsync());
        }
    }
}
