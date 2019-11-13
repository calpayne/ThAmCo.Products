using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThAmCo.Products.Data
{
    public class StoreDb : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<PriceHistory> PriceHistory { get; set; }

        public StoreDb(DbContextOptions<StoreDb> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(x =>
            {
                x.Property(p => p.Name).IsRequired();
                x.Property(p => p.Description).IsRequired();
                x.Property(p => p.Price).IsRequired();
                x.Property(p => p.StockLevel).HasDefaultValue(0);
                x.Property(p => p.BrandId).IsRequired();
                x.Property(p => p.CategoryId).IsRequired();
            });

            modelBuilder.Entity<PriceHistory>(x =>
            {
                x.Property(p => p.Price).IsRequired();
                x.Property(p => p.CreatedDate).IsRequired();
                x.HasOne(p => p.Product).WithMany()
                                         .HasForeignKey(p => p.ProductId)
                                         .IsRequired();
            });

            // Honour soft delete
            modelBuilder.Entity<Product>().HasQueryFilter(p => EF.Property<bool>(p, "Active") == true);
        }
    }
}
