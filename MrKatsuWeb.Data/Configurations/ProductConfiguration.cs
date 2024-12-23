using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrKatsuWeb.Data.Entities;
using MrKatsuWeb.Data.Enums;

namespace MrKatsuWeb.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.ProductCode).IsRequired().HasMaxLength(20);
            builder.Property(x => x.ProductName).IsRequired().IsUnicode(true).HasMaxLength(200);
            builder.Property(x => x.SeoAlias).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Support).HasDefaultValue(ProductStatus.LongTermSupport);
            builder.Property(x => x.Status).HasDefaultValue(true);
            builder.Property(x => x.LikeCount).HasDefaultValue(0);
            builder.Property(x => x.DislikeCount).HasDefaultValue(0);
            builder.Property(x => x.ViewCount).HasDefaultValue(0);
            builder.Property(x => x.DownloadCount).HasDefaultValue(0);
            builder.Property(x => x.PromotionPrice).HasPrecision(18, 2).HasDefaultValue(0);
            builder.Property(x => x.OriginalPrice).HasPrecision(18, 2).HasDefaultValue(0);
            builder.Property(x => x.Version).HasDefaultValue("1.0.0");
            builder.Property(x => x.CreateTime).HasDefaultValue(DateTime.UtcNow);

            builder.Property(x => x.SeoDescription).IsUnicode(true).HasMaxLength(500);
            builder.Property(x => x.Detail).IsUnicode(true);
        }
    }
}
