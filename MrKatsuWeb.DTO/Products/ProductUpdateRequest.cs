using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrKatsuWeb.DTO.Products
{
    public class ProductUpdateRequest
    {
        public int? CategoryId { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? SeoAlias { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoKeyword { get; set; }
        public string? OriginalPrice { get; set; }
        public string? PromotionPrice { get; set; }
        public string? Detail { get; set; }
        public string? Support { get; set; }
        public string? Image { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public int ViewCount { get; set; }
        public int DownloadCount { get; set; }
        public bool Status { get; set; }
    }
}
