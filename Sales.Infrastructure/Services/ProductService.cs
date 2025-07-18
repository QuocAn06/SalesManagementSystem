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
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context; // Assuming ApplicationDbContext is your EF Core DbContext for the Sales domain
        private readonly IMapper _mapper; // Assuming you are using AutoMapper for mapping between entities and DTOs

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all products
        public async Task<List<ProductDto>> GetAllProductAsync()
        {
            var products = await _context.Products.ToListAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return productDtos;
        }

        // Get product by ID
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        // Create a new product
        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var entity = _mapper.Map<Product>(productDto);
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            var createdDto = _mapper.Map<ProductDto>(entity);
            return createdDto;
        }

        // Update an existing product
        public async Task<ProductDto> UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _context.Products.FindAsync(productDto.Id);
            if (product == null)
            {
                return null;
            }

            // Map the updated properties from DTO to entity
            _mapper.Map(productDto, product);
            await _context.SaveChangesAsync();
            var updatedDto = _mapper.Map<ProductDto>(product);
            return updatedDto;
        }

        // Delete a product by ID
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}
