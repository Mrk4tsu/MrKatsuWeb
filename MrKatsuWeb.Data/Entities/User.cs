using Microsoft.AspNetCore.Identity;

namespace MrKatsuWeb.Data.Entities
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public DateTime LastLogin { get; set; }
        public string Avatar { get; set; }
        public bool Status { get; set; }
        public List<Product> Products { get; set; }
    }
}
