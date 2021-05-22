using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> ListAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task Delete(Product product);
    }
}
