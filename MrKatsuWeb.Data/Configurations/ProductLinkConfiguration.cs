using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrKatsuWeb.Data.Entities;

namespace MrKatsuWeb.Data.Configurations
{
    public class ProductLinkConfiguration : IEntityTypeConfiguration<ProductLink>
    {
        public void Configure(EntityTypeBuilder<ProductLink> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Link).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.Status).HasDefaultValue(true);

            builder.HasOne(x => x.Product).WithMany(x => x.ProductLinks).HasForeignKey(x => x.ProductId);
        }
    }
}