using System;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        protected Product() { }

        public Product(string name, string description, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name cannot be empty");

            if (price < 0)
                throw new DomainException("Product price cannot be negative");

            if (stock < 0)
                throw new DomainException("Product stock cannot be negative");

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }

        public void UpdateDetails(string name, string description, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name cannot be empty");

            if (price < 0)
                throw new DomainException("Product price cannot be negative");

            Name = name;
            Description = description;
            Price = price;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStock(int newStock)
        {
            if (newStock < 0)
                throw new DomainException("Product stock cannot be negative");

            Stock = newStock;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}