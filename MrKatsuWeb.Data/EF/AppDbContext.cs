using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MrKatsuWeb.Data.Configurations;
using MrKatsuWeb.Data.Entities;

namespace MrKatsuWeb.Data.EF
{
    public class AppDbContext : IdentityDbContext<User, Role, int>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductLinkConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("appuserclaims");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("appuserroleclaims").HasKey(x => new
            {
                x.RoleId,
                x.UserId
            });
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("appuserlogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("approlesclaims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("usertokens").HasKey(x => x.UserId);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLink> ProductLinks { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
