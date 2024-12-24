using Microsoft.AspNetCore.Http;
using MrKatsuWeb.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrKatsuWeb.DTO.Products
{
    public class ProductCreateRequest
    {
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeyword { get; set; }
        public decimal OriginalPrice { get; set; }
        public string Detail { get; set; }
        public string Version { get; set; }
        public ProductStatus Support { get; set; }
        public IFormFile Image { get; set; }
        public List<string> Link { get; set; }
    }
}
