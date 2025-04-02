using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly Dictionary<Guid, Product> _products = new();

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_products.TryGetValue(id, out var product) ? product : null);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await Task.FromResult(_products.Values);
        }

        public async Task<Product> AddAsync(Product product)
        {
            _products.Add(product.Id, product);
            return await Task.FromResult(product);
        }

        public async Task UpdateAsync(Product product)
        {
            if (_products.ContainsKey(product.Id))
            {
                _products[product.Id] = product;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            _products.Remove(id);
            await Task.CompletedTask;
        }
    }
}