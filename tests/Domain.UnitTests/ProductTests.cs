using System;
using Xunit;
using Domain.Entities;
using Domain.Exceptions;

namespace Domain.UnitTests
{
    public class ProductTests
    {
        [Fact]
        public void Create_WithValidData_ShouldCreateProduct()
        {
            // Arrange
            var name = "Test Product";
            var description = "Test Description";
            var price = 10.99m;
            var stock = 100;

            // Act
            var product = new Product(name, description, price, stock);

            // Assert
            Assert.Equal(name, product.Name);
            Assert.Equal(description, product.Description);
            Assert.Equal(price, product.Price);
            Assert.Equal(stock, product.Stock);
            Assert.NotEqual(Guid.Empty, product.Id);
            Assert.NotNull(product.CreatedAt);
        }

        [Fact]
        public void Create_WithEmptyName_ShouldThrowException()
        {
            // Arrange
            var name = "";
            var description = "Test Description";
            var price = 10.99m;
            var stock = 100;

            // Act & Assert
            Assert.Throws<DomainException>(() => new Product(name, description, price, stock));
        }

        [Fact]
        public void Create_WithNegativePrice_ShouldThrowException()
        {
            // Arrange
            var name = "Test Product";
            var description = "Test Description";
            var price = -10.99m;
            var stock = 100;

            // Act & Assert
            Assert.Throws<DomainException>(() => new Product(name, description, price, stock));
        }

        [Fact]
        public void Create_WithNegativeStock_ShouldThrowException()
        {
            // Arrange
            var name = "Test Product";
            var description = "Test Description";
            var price = 10.99m;
            var stock = -100;

            // Act & Assert
            Assert.Throws<DomainException>(() => new Product(name, description, price, stock));
        }

        [Fact]
        public void UpdateDetails_WithValidData_ShouldUpdateProduct()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description", 10.99m, 100);
            var newName = "Updated Product";
            var newDescription = "Updated Description";
            var newPrice = 20.99m;

            // Act
            product.UpdateDetails(newName, newDescription, newPrice);

            // Assert
            Assert.Equal(newName, product.Name);
            Assert.Equal(newDescription, product.Description);
            Assert.Equal(newPrice, product.Price);
            Assert.NotNull(product.UpdatedAt);
        }

        [Fact]
        public void UpdateDetails_WithEmptyName_ShouldThrowException()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description", 10.99m, 100);
            var newName = "";
            var newDescription = "Updated Description";
            var newPrice = 20.99m;

            // Act & Assert
            Assert.Throws<DomainException>(() => product.UpdateDetails(newName, newDescription, newPrice));
        }

        [Fact]
        public void UpdateDetails_WithNegativePrice_ShouldThrowException()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description", 10.99m, 100);
            var newName = "Updated Product";
            var newDescription = "Updated Description";
            var newPrice = -20.99m;

            // Act & Assert
            Assert.Throws<DomainException>(() => product.UpdateDetails(newName, newDescription, newPrice));
        }

        [Fact]
        public void UpdateStock_WithValidData_ShouldUpdateStock()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description", 10.99m, 100);
            var newStock = 200;

            // Act
            product.UpdateStock(newStock);

            // Assert
            Assert.Equal(newStock, product.Stock);
            Assert.NotNull(product.UpdatedAt);
        }

        [Fact]
        public void UpdateStock_WithNegativeStock_ShouldThrowException()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description", 10.99m, 100);
            var newStock = -200;

            // Act & Assert
            Assert.Throws<DomainException>(() => product.UpdateStock(newStock));
        }
    }
}