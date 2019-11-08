using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThAmCo.Products.Data
{
    public class StoreDb : DbContext
    {
        public DbSet<Material> Materials { get; set; }
        public DbSet<PType> Types { get; set; }
        public DbSet<Brand> Brands { get; set; }
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

            modelBuilder.Entity<Material>(x =>
            {
                x.Property(m => m.Name).IsRequired();
            });

            modelBuilder.Entity<PType>(x =>
            {
                x.Property(t => t.Name).IsRequired();
                x.Property(t => t.Description).IsRequired();
            });

            modelBuilder.Entity<Brand>(x =>
            {
                x.Property(b => b.Name).IsRequired();
                x.Property(b => b.Description).IsRequired();
            });

            modelBuilder.Entity<Product>(x =>
            {
                x.Property(p => p.Name).IsRequired();
                x.Property(p => p.Description).IsRequired();
                x.Property(p => p.Price).IsRequired();
                x.Property(p => p.StockLevel).HasDefaultValue(0);
                x.HasOne(p => p.Type).WithMany()
                                         .HasForeignKey(p => p.TypeId)
                                         .IsRequired();
                x.HasOne(p => p.Material).WithMany()
                                         .HasForeignKey(p => p.MaterialId)
                                         .IsRequired();
                x.HasOne(p => p.Brand).WithMany()
                                      .HasForeignKey(p => p.BrandId)
                                      .IsRequired();
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
            modelBuilder.Entity<Material>().HasQueryFilter(m => EF.Property<bool>(m, "Active") == true);
            modelBuilder.Entity<PType>().HasQueryFilter(t => EF.Property<bool>(t, "Active") == true);
            modelBuilder.Entity<Brand>().HasQueryFilter(b => EF.Property<bool>(b, "Active") == true);
            modelBuilder.Entity<Product>().HasQueryFilter(p => EF.Property<bool>(p, "Active") == true);
        }
    }
}
