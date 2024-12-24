namespace MrKatsuWeb.DTO.Products
{
    public class LinkCreateRequest
    {
        public int ProductId { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
    }
}