using MrKatsuWeb.Data.Entities;
using MrKatsuWeb.DTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrKatsuWeb.Application.Interfaces.Manage
{
    public interface IProductManageService
    {
        Task<Product?> GetById(int productId);
        IQueryable<Product> @query();
        Task<List<ProductViewModel>?> GetProducts();
        Task<int> AddProduct(ProductCreateRequest request);
        Task<int> UpdateProduct(ProductUpdateRequest request);
        Task<bool> DeleteProduct(int productId);
        Task<bool> IsExisting(string productCode);
        Task<bool> AddLinkProduct(LinkCreateRequest request);
    }
}
