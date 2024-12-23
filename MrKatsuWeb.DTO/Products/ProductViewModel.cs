namespace MrKatsuWeb.DTO.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string OriginalPrice { get; set; }
        public decimal PromotionPrice { get; set; }
        public string SeoAlias { get; set; }
        public string Version { get; set; }
        public string Support { get; set; }
        public string Image { get; set; }
        public int ViewCount { get; set; }
        public int DownloadCount { get; set; }
        public int CategoryId { get; set; }
        public string DateCreate { get; set; }
        public string DateUpdate { get; set; }
    }
}
