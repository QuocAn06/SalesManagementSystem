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
        Task<List<ProductDto>> GetAllProductAsync();
        
        // Get product by ID
        Task<ProductDto> GetProductByIdAsync(int id);

        // Create a new product
        Task<ProductDto> CreateProductAsync(ProductDto productDto);

        // Delete a product by ID
        Task<ProductDto> UpdateProductAsync(int id, ProductDto productDto);

        // Delete a product by ID
        Task<bool> DeleteProductAsync(int id);
    }
}
