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
        }
        public DbSet<Product> Products { get; set; }
    }
}
