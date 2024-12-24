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
                DateUpload = StringHelper.DateShowInCard(x.CreateTime, x.UpdateTime),
                ViewCount = x.ViewCount,
                DownloadCount = x.DownloadCount
            });
            if (await q.CountAsync() <= 0)
                return null;
            return await q.OrderByDescending(x => x.DateUpload).ToListAsync();
        }
        public async Task<int> AddProduct(ProductCreateRequest request)
        {
            var productCode = StringHelper.GenerateGuid(9);
            string? resultImage = "";
            if (request.Image != null)
            {
                string folder = $"{FOLDER}/{productCode}";
                resultImage = await imageService.SaveImage(request.Image, request.ProductName, folder);
            }

            if (resultImage == null) return -1;
            var product = new Product
            {
                CategoryId = request.CategoryId,
                ProductCode = productCode,
                ProductName = request.ProductName,
                OriginalPrice = request.OriginalPrice,
                PromotionPrice = 0,
                SeoAlias = StringHelper.CreateSeoAlias(request.ProductName),
                SeoDescription = $"Khám phá {request.ProductName} - đã xuất hiện tại MrKatsu Website, miễn phí tải xuống! Cung cấp công cụ mạnh mẽ, dễ sử dụng, phù hợp cho mọi thành viên. Truy cập ngay để tải về!",
                SeoKeyword = $"Tải xuống {request.ProductName} miễn phí",
                SeoTitle = $"Truy cập ngay trang web MrKatsu để tải {request.ProductName} miễn phí",
                Detail = request.Detail,
                Version = request.Version,
                Support = request.Support,
                Image = resultImage,
                Status = true,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                ProductLinks = new List<ProductLink>(),
                ProductImages = new List<ProductImage>()
            };
            if (request.Images != null && request.Images.Count > 0)
            {
                for (int i = 0; i < request.Images.Count; i++)
                {
                    var publicId = request.ProductName + i;
                    var resultUpload = await imageService.SaveImage(request.Images[i], publicId, $"{FOLDER}/{product.ProductCode}");
                    if (resultUpload == null) return -5;

                    // Thêm ảnh vào danh sách ProductImages
                    product.ProductImages.Add(new ProductImage
                    {
                        //wwwroot/ABC-123/ABC-123-randomstring
                        publicId = $"{publicId}",
                        Path = resultUpload,
                        ProductId = product.Id,
                        Caption = $"Ảnh {request.ProductName} {i + 1}",
                        SortOrder = i
                    });
                }
            }
            if (request.Link != null)
            {
                foreach (var link in request.Link)
                {
                    product.ProductLinks.Add(new ProductLink
                    {
                        ProductId = product.Id,
                        Link = link ?? string.Empty,
                        Description = $"Link tải {DateTime.Now.ToString("dd/MM/yyyy")}",
                        Title = $"{StringHelper.CreateSeoAlias(product.ProductName)}-{product.Version}{link.IndexOf(link)}",
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
            var productImages = await db.ProductImages.Where(x => x.ProductId == product.Id).ToListAsync();
            await imageService.DeleteImage(product.ProductName, folder);

            foreach (var image in productImages)
            {
                await imageService.DeleteImage(image.publicId, folder);
            }

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
