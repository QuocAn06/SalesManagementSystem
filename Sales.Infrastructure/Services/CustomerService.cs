using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales.Application.DTOs;
using Sales.Application.Interfaces;
using Sales.Domain.Entities;
using Sales.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context; // Assuming ApplicationDbContext is your EF Core DbContext for the Sales domain
        private readonly IMapper _mapper; // Assuming you are using AutoMapper for mapping between entities and DTOs

        public CustomerService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all customers
        public async Task<List<CustomerDto>> GetAllCustomerAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            var customerDtos = _mapper.Map<List<CustomerDto>>(customers);
            return customerDtos;
        }

        // Get customer by ID
        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return null;
            }

            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;
        }

        // Create a new customer
        public async Task<CustomerDto> CreateNewCustomerAsync(CustomerDto customerDto)
        {
            var entity = _mapper.Map<Customer>(customerDto);
            _context.Customers.Add(entity);
            await _context.SaveChangesAsync();
            var createdDto = _mapper.Map<CustomerDto>(entity);
            return createdDto;
        }

        // Update an existing customer
        public async Task<CustomerDto> UpdateCustomerAsync(int id, CustomerDto customerDto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return null;
            }

            // Map the updated properties from DTO to entity
            _mapper.Map(customerDto, customer);
            await _context.SaveChangesAsync();
            var updatedDto = _mapper.Map<CustomerDto>(customer);
            return updatedDto;
        }

        // Delete a product by ID
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            
            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}
