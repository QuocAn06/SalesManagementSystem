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
        Task<List<CustomerDto>> GetAllAsync();

        // Get Infomatiom of Customer by ID
        Task<CustomerDto> GetByIdAsync(int id);
        
        // Create new Infomation of Customer
        Task<CustomerDto> CreateAsync(CustomerDto customerDto);

        // Update Infomation of Customer
        Task<CustomerDto> UpdateAsync(int customerId, CustomerDto customerDto);

        // Delete Infomation
        Task<bool> DeleteAsync(int customerId);
    }
}
