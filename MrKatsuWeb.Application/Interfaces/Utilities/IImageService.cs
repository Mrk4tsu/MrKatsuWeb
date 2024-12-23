using Microsoft.AspNetCore.Http;

namespace MrKatsuWeb.Application.Interfaces.Utilities
{
    public interface IImageService
    {
        Task<string?> SaveImage(IFormFile file);
        Task<string?> SaveImage(IFormFile file, string publicId, string folder);
        MemoryStream ConvertToWebP(IFormFile file);
    }
}
