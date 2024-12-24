namespace MrKatsuWeb.Data.Entities{
    public class ProductLink{
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Link { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }
        public Product Product { get; set; }
    }
}