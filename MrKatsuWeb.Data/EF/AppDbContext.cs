using Microsoft.EntityFrameworkCore;
using MrKatsuWeb.Data.Configurations;
using MrKatsuWeb.Data.Entities;

namespace MrKatsuWeb.Data.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductLinkConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLink> ProductLinks { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
