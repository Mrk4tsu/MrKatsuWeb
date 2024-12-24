namespace MrKatsuWeb.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string publicId { get; set; }
        public int ProductId { get; set; }
        public string Path { get; set; }
        public string Caption { get; set; }
        public int SortOrder { get; set; }
        public Product Product { get; set; }
    }
}