using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database.Models;

namespace Database.Context
{
    public class InventaryAppDbContext : DbContext
    {
        public InventaryAppDbContext(DbContextOptions<InventaryAppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<Product> product = new();

            string[] prod = { "Frijol", "Arroz", "Garbanzo", "Pasta", "Papa", "Aceite"};

            Random rd = new();
            int limit = rd.Next(10, 20);
            for (int i = 0; i <= limit; i++)
            {
                product.Add(new Product {
                    Id = i+1,
                    Name = prod[rd.Next(0,prod.Length)]
                });
            }

            modelBuilder.Entity<Product>().HasData(product.ToArray());
        }

    }
}