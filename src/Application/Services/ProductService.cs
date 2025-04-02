using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System.Linq;

namespace Application.Services
{
    /// <summary>
    /// Service responsible for handling product-related operations
    /// </summary>
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the ProductService
        /// </summary>
        /// <param name="productRepository">The product repository dependency</param>
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Retrieves a product by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <returns>The product DTO if found, null otherwise</returns>
        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product == null ? null : MapToDto(product);
        }

        /// <summary>
        /// Retrieves all products from the system
        /// </summary>
        /// <returns>A collection of product DTOs</returns>
        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return MapToDto(products);
        }

        /// <summary>
        /// Creates a new product in the system
        /// </summary>
        /// <param name="name">The name of the product</param>
        /// <param name="description">The description of the product</param>
        /// <param name="price">The price of the product</param>
        /// <param name="stock">The initial stock quantity</param>
        /// <returns>The created product DTO</returns>
        /// <exception cref="DomainException">Thrown when the product data is invalid</exception>
        public async Task<ProductDto> CreateAsync(string name, string description, decimal price, int stock)
        {
            var product = new Product(name, description, price, stock);
            await _productRepository.AddAsync(product);
            return MapToDto(product);
        }

        /// <summary>
        /// Updates an existing product's details
        /// </summary>
        /// <param name="id">The unique identifier of the product to update</param>
        /// <param name="name">The new name of the product</param>
        /// <param name="description">The new description of the product</param>
        /// <param name="price">The new price of the product</param>
        /// <exception cref="Exception">Thrown when the product is not found</exception>
        /// <exception cref="DomainException">Thrown when the update data is invalid</exception>
        public async Task UpdateAsync(Guid id, string name, string description, decimal price)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new Exception("Product not found");

            product.UpdateDetails(name, description, price);
            await _productRepository.UpdateAsync(product);
        }

        /// <summary>
        /// Updates the stock quantity of an existing product
        /// </summary>
        /// <param name="id">The unique identifier of the product to update</param>
        /// <param name="newStock">The new stock quantity</param>
        /// <exception cref="Exception">Thrown when the product is not found</exception>
        /// <exception cref="DomainException">Thrown when the new stock quantity is invalid</exception>
        public async Task UpdateStockAsync(Guid id, int newStock)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new Exception("Product not found");

            product.UpdateStock(newStock);
            await _productRepository.UpdateAsync(product);
        }

        /// <summary>
        /// Deletes a product from the system
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete</param>
        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name ?? string.Empty,
                Description = product.Description ?? string.Empty,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        private static IEnumerable<ProductDto> MapToDto(IEnumerable<Product> products)
        {
            return products.Select(MapToDto).ToList();
        }
    }
}