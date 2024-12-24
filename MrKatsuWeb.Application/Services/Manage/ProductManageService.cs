using Microsoft.EntityFrameworkCore;
using MrKatsuWeb.Application.Interfaces.Manage;
using MrKatsuWeb.Application.Interfaces.Utilities;
using MrKatsuWeb.Common;
using MrKatsuWeb.Data.EF;
using MrKatsuWeb.Data.Entities;
using MrKatsuWeb.DTO.Products;

namespace MrKatsuWeb.Application.Services.Manage
{
    public class ProductManageService : IProductManageService
    {
        private readonly AppDbContext db;
        private const string FOLDER = "product";
        private readonly IImageService imageService;
        public ProductManageService(AppDbContext db, IImageService imageService)
        {
            this.db = db;
            this.imageService = imageService;
        }
        public IQueryable<Product> @query()
        {
            var query = from product in db.Products
                        select product;
            return query;
        }
        public async Task<Product?> GetById(int productId)
        {
            var product = await query().FirstOrDefaultAsync(x => x.Id == productId);
            return product;
        }
        public async Task<List<ProductViewModel>?> GetProducts()
        {
            var q = query().Select(x => new ProductViewModel
            {
                CategoryId = x.CategoryId,
                Id = x.Id,
                Image = x.Image,
                OriginalPrice = StringHelper.GetAmount(x.OriginalPrice),
                ProductCode = x.ProductCode,
                ProductName = x.ProductName,
                PromotionPrice = x.PromotionPrice,
                SeoAlias = x.SeoAlias,
                Support = x.Support.ToString(),
                Version = x.Version,
                DateCreate = x.CreateTime.ToString(),
                DateUpdate = x.UpdateTime.HasValue ? x.UpdateTime.Value.ToString() : "N/A",
                ViewCount = x.ViewCount,
                DownloadCount = x.DownloadCount
            });
            if (await q.CountAsync() <= 0)
                return null;
            return await q.OrderByDescending(x => x.DateCreate).ToListAsync();
        }
        public async Task<int> AddProduct(ProductCreateRequest request)
        {
            var productCode = StringHelper.GenerateGuid(9);
            string? resultImage = "";
            if (request.Image != null)
            {
                string folder = $"{FOLDER}/{productCode}";
                resultImage = await imageService.SaveImage(request.Image, productCode, folder);
            }

            if (resultImage == null) return -1;
            var product = new Product
            {
                CategoryId = request.CategoryId,
                ProductCode = StringHelper.GenerateGuid(9),
                ProductName = request.ProductName,
                OriginalPrice = request.OriginalPrice,
                PromotionPrice = 0,
                SeoAlias = StringHelper.CreateSeoAlias(request.ProductName),
                SeoDescription = request.SeoDescription,
                SeoKeyword = request.SeoKeyword,
                SeoTitle = request.SeoTitle,
                Detail = request.Detail,
                Version = request.Version,
                Support = request.Support,
                Image = resultImage,
                Status = true,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                ProductLinks = new List<ProductLink>()
            };
            if(request.Link != null)
            {
                foreach (var link in request.Link)
                {
                    product.ProductLinks.Add(new ProductLink
                    {
                        ProductId = product.Id,
                        Link = link,
                        Description = $"Link tải {DateTime.Now.ToString("dd/MM/yyyy")}",
                        Title = product.ProductName,
                        Status = true
                    });
                }
            }
            db.Products.Add(product);
            await db.SaveChangesAsync();

            return product.Id;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await query().FirstOrDefaultAsync(x => x.Id == productId);
            if (product == null) return false;
            //product/123
            string folder = $"{FOLDER}/{product.ProductCode}";
            var image = product.Image;
            await imageService.DeleteImage(image, folder);
            await imageService.DeleteFolder($"{FOLDER}/{product.ProductCode}");

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return true;
        }





        public Task<bool> IsExisting(string productCode)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateProduct(ProductUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddLinkProduct(LinkCreateRequest request)
        {
            var product = query().FirstOrDefault(x => x.Id == request.ProductId);
            if (product == null) return false;
            var link = new ProductLink
            {
                ProductId = request.ProductId,
                Link = request.Link,
                Description = request.Description,
                Title = $"Link tải {DateTime.Now.ToString("dd/MM/yyyy")}",
                Status = true
            };
            db.ProductLinks.Add(link);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
