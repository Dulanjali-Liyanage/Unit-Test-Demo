using Microsoft.EntityFrameworkCore;
using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure
{
    public class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
    }
}
