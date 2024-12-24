using MrKatsuWeb.Data.Enums;

namespace MrKatsuWeb.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string SeoAlias { get; set; } = string.Empty;
        public string SeoDescription { get; set; } = string.Empty;
        public string SeoTitle { get; set; } = string.Empty;
        public string SeoKeyword { get; set; } = string.Empty;
        public decimal OriginalPrice { get; set; }
        public decimal PromotionPrice { get; set; }
        public string Detail { get; set; } = string.Empty;
        public ProductStatus Support { get; set; }
        public string Image { get; set; } = string.Empty;
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public int ViewCount { get; set; }
        public int DownloadCount { get; set; }
        public string Version { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Status { get; set; }
        public List<ProductLink> ProductLinks { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public User User { get; set; }
    }
}
