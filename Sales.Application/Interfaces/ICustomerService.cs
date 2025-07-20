using Sales.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Interfaces
{
    public interface ICustomerService
    {
        // Get All Infomation of Customers
        Task<List<CustomerDto>> GetAllCustomerAsync();

        // Get Infomatiom of Customer by ID
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        
        // Create new Infomation of Customer
        Task<CustomerDto> CreateNewCustomerAsync(CustomerDto customerDto);

        // Update Infomation of Customer
        Task<CustomerDto> UpdateCustomerAsync(int customerId, CustomerDto customerDto);

        // Delete Infomation
        Task<bool> DeleteCustomerAsync(int customerId);
    }
}
