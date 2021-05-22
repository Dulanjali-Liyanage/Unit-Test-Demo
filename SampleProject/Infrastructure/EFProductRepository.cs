using Microsoft.EntityFrameworkCore;
using SampleProject.Interfaces;
using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure
{
    public class EFProductRepository : IProductRepository
    {
        private readonly SampleContext _dbContext;

        public EFProductRepository(SampleContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Product> GetByIdAsync(int id)
        {
            return _dbContext.Product.FirstOrDefaultAsync(s => s.ProductId == id);
        }

        public Task<List<Product>> ListAsync()
        {
            return _dbContext.Product.ToListAsync();
        }

        public Task AddAsync(Product product)
        {
            _dbContext.Product.Add(product);
            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(Product product)
        {
            _dbContext.Update(product);
            return _dbContext.SaveChangesAsync();
        }

        public Task Delete(Product product)
        {
            _dbContext.Product.Remove(product);
            return _dbContext.SaveChangesAsync();
        }
    }
}
