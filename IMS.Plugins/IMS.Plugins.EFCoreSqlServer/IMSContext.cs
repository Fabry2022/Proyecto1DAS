﻿using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class IMSContext : DbContext
    {
        public IMSContext(DbContextOptions<IMSContext> options): base(options)
        {
            
        }

        public DbSet<Inventory>? Inventories { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<ProductInventory>? ProductInventories { get; set; }
        public DbSet<InventoryTransaction>? InventoryTransactions { get; set; }
        public DbSet<ProductTransaction>? ProductTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //build relationships
            modelBuilder.Entity<ProductInventory>()
                .HasKey(pi => new { pi.ProductId, pi.InventoryId });

            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductInventories)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Inventory)
                .WithMany(i => i.ProductInventories)
                .HasForeignKey(pi => pi.InventoryId);

            //seeding data
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { InventoryId = 1, InventoryName = "Laptop", Quantity = 10, Price = 500 },
                new Inventory { InventoryId = 2, InventoryName = "PC", Quantity = 10, Price = 150 },
                new Inventory { InventoryId = 3, InventoryName = "Smartphone", Quantity = 20, Price = 800 },
                new Inventory { InventoryId = 4, InventoryName = "Tablet", Quantity = 20, Price = 150 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product() { ProductId = 1, ProductName = "PC", Quantity = 10, Price = 150 },
                new Product() { ProductId = 2, ProductName = "Mobile", Quantity = 5, Price = 25000 }
            );

            modelBuilder.Entity<ProductInventory>().HasData(
                new ProductInventory { ProductId = 1, InventoryId = 1, InventoryQuantity = 1 }, 
                new ProductInventory { ProductId = 1, InventoryId = 2, InventoryQuantity = 1 }, 
                new ProductInventory { ProductId = 1, InventoryId = 3, InventoryQuantity = 2 }, 
                new ProductInventory { ProductId = 1, InventoryId = 4, InventoryQuantity = 2 } 
            );
        }
    }
}