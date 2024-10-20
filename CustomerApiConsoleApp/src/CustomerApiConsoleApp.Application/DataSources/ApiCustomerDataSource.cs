using CustomerApiConsoleApp.Application.Dto;
using CustomerApiConsoleApp.Application.Interfaces;
using CustomerApiConsoleApp.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CustomerApiConsoleApp.Application.DataSources
{
    public class ApiCustomerDataSource : ICustomerDataSource
    {
        private readonly HttpClient _httpClient;
        private readonly string _customerApiUrl;

        public ApiCustomerDataSource(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _customerApiUrl = configuration["ApiSettings:CustomerApiUrl"];
        }

        public async Task<List<Customer>> FetchCustomersAsync()
        {
            var response = await _httpClient.GetAsync(_customerApiUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var customerDtos = JsonSerializer.Deserialize<List<CustomerDto>>(json);

            return customerDtos.Select(dto => new Customer
            {
                FirstName = dto.first_name,
                LastName = dto.last_name,
                Email = dto.email
            }).ToList();
        }
    }
}
