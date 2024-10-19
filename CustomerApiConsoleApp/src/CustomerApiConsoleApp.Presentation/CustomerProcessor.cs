using CustomerApiConsoleApp.Application.Interfaces;
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

        public CustomerProcessor(ICustomerService customerService, ICustomerRepository customerRepository)
        {
            _customerService = customerService;
            _customerRepository = customerRepository;
        }

        public async Task ProcessCustomersAsync()
        {
            var customers = await _customerService.GetCustomersAsync();

            foreach (var customer in customers)
            {
                await _customerRepository.AddCustomerAsync(customer);
            }

            await _customerRepository.SaveChangesAsync();

            Console.WriteLine("Customers have been added to the database successfully.");
        }
    }
}
