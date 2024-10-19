using CustomerApiConsoleApp.Application.Dto;
using CustomerApiConsoleApp.Application.Interfaces;
using CustomerApiConsoleApp.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CustomerApiConsoleApp.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly string _customerApiUrl;

        public CustomerService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _customerApiUrl = configuration["ApiSettings:CustomerApiUrl"];
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var response = await _httpClient.GetAsync(_customerApiUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var customerDtos = JsonSerializer.Deserialize<List<CustomerDto>>(json);

            // Map DTO to Domain Customer
            var customers = new List<Customer>();
            foreach (var customerDto in customerDtos)
            {
                customers.Add(new Customer
                {
                    FirstName = customerDto.first_name,
                    LastName = customerDto.last_name,
                    Email = customerDto.email
                });
            }

            return customers;
        }
    }
}