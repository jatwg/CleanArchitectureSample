using CustomerApiConsoleApp.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiConsoleApp.Presentation
{
    public class CustomerProcessor
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerProcessor> _logger;

        public CustomerProcessor(ICustomerService customerService, ICustomerRepository customerRepository, ILogger<CustomerProcessor> logger)
        {
            _customerService = customerService;
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task ProcessCustomersAsync()
        {
            var customers = await _customerService.GetCustomersAsync();
            
            foreach (var customer in customers)
            {
                await _customerRepository.AddCustomerAsync(customer);
            }

            await _customerRepository.SaveChangesAsync();

        }
    }
}
