using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrKatsuWeb.Application.Interfaces.Manage;
using MrKatsuWeb.Utilities;

namespace MrKatsuWeb.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductMangesController : ControllerBase
    {
        private readonly IProductManageService service;
        public ProductMangesController(IProductManageService productService)
        {
            this.service = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await service.GetProducts();
                if (products == null)
                {
                    return APIRespone.NotFound("Không có sản phẩm nào ở đây cả!");
                }
                return APIRespone.Success(products);
            }
            catch (Exception ex)
            {
                // Trả về lỗi chi tiết
                return APIRespone.Error(ex.Message, CodeStatus.InternalServerError);
            }
        }
    }
}
