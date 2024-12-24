using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrKatsuWeb.Data.Entities;

namespace MrKatsuWeb.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.publicId).IsRequired().HasMaxLength(255);
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Path).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Caption).HasMaxLength(255);
            builder.Property(x => x.SortOrder).IsRequired();
            builder.HasOne(x => x.Product).WithMany(x => x.ProductImages).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}