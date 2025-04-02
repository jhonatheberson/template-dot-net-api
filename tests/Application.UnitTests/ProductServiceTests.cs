using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

namespace Application.UnitTests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _service = new ProductService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetById_WhenProductExists_ShouldReturnProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product("Test Product", "Test Description", 10.99m, 100);
            _mockRepository.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync(product);

            // Act
            var result = await _service.GetByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Description, result.Description);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.Stock, result.Stock);
        }

        [Fact]
        public async Task GetById_WhenProductDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _service.GetByIdAsync(productId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product("Product 1", "Description 1", 10.99m, 100),
                new Product("Product 2", "Description 2", 20.99m, 200)
            };
            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(products);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Create_ShouldCreateProduct()
        {
            // Arrange
            var name = "Test Product";
            var description = "Test Description";
            var price = 10.99m;
            var stock = 100;
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync((Product p) => p);

            // Act
            var result = await _service.CreateAsync(name, description, price, stock);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(name, result.Name);
            Assert.Equal(description, result.Description);
            Assert.Equal(price, result.Price);
            Assert.Equal(stock, result.Stock);
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task Update_WhenProductExists_ShouldUpdateProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product("Test Product", "Test Description", 10.99m, 100);
            _mockRepository.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync(product);

            // Act
            await _service.UpdateAsync(productId, "Updated Name", "Updated Description", 20.99m);

            // Assert
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task Update_WhenProductDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync((Product)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                _service.UpdateAsync(productId, "Updated Name", "Updated Description", 20.99m));
        }

        [Fact]
        public async Task UpdateStock_WhenProductExists_ShouldUpdateStock()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product("Test Product", "Test Description", 10.99m, 100);
            _mockRepository.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync(product);

            // Act
            await _service.UpdateStockAsync(productId, 200);

            // Assert
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task UpdateStock_WhenProductDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync((Product)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                _service.UpdateStockAsync(productId, 200));
        }

        [Fact]
        public async Task Delete_ShouldDeleteProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();

            // Act
            await _service.DeleteAsync(productId);

            // Assert
            _mockRepository.Verify(r => r.DeleteAsync(productId), Times.Once);
        }
    }
}