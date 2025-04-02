using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return MapToDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return MapToDto(products);
        }

        public async Task<ProductDto> CreateAsync(string name, string description, decimal price, int stock)
        {
            var product = new Product(name, description, price, stock);
            await _productRepository.AddAsync(product);
            return MapToDto(product);
        }

        public async Task UpdateAsync(Guid id, string name, string description, decimal price)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new Exception("Product not found");

            product.UpdateDetails(name, description, price);
            await _productRepository.UpdateAsync(product);
        }

        public async Task UpdateStockAsync(Guid id, int newStock)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new Exception("Product not found");

            product.UpdateStock(newStock);
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        private static ProductDto? MapToDto(Product? product)
        {
            if (product == null)
                return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        private static IEnumerable<ProductDto> MapToDto(IEnumerable<Product> products)
        {
            var dtos = new List<ProductDto>();
            foreach (var product in products)
            {
                var dto = MapToDto(product);
                if (dto != null)
                {
                    dtos.Add(dto);
                }
            }
            return dtos;
        }
    }
}