using Microsoft.EntityFrameworkCore;

namespace ThAmCo.Products.Data
{
    public class StoreDb : DbContext
    {
        public DbSet<ProductStock> ProductStock { get; set; }
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

            modelBuilder.Entity<ProductStock>(x =>
            {
                x.Property(p => p.ProductId).IsRequired();
                x.Property(p => p.StockLevel).IsRequired();
                x.HasIndex(p => p.ProductId).IsUnique();
            });

            modelBuilder.Entity<PriceHistory>(x =>
            {
                x.Property(p => p.ProductId).IsRequired();
                x.Property(p => p.Price).IsRequired();
                x.Property(p => p.CreatedDate).IsRequired();
            });
        }
    }
}
