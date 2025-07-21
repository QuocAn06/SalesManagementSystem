using Sales.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Interfaces
{
    public interface IProductService
    {
        // Get all products
        Task<List<ProductDto>> GetAllAsync();
        
        // Get product by ID
        Task<ProductDto> GetByIdAsync(int id);

        // Create a new product
        Task<ProductDto> CreateAsync(ProductDto productDto);

        // Delete a product by ID
        Task<ProductDto> UpdateAsync(int productId, ProductDto productDto);

        // Delete a product by ID
        Task<bool> DeleteAsync(int id);
    }
}
