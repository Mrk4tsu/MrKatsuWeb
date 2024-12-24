using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using MrKatsuWeb.Application.Interfaces.Utilities;

namespace MrKatsuWeb.Application.Services
{
    public class ImageService : IImageService
    {
        private const string ROOT_FOLDER = "public";
        private readonly Cloudinary _cloudinary;
        public ImageService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string?> SaveImage(IFormFile file)
        {
            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Overwrite = true,
                    Format = "webp",
                    Transformation = new Transformation().Width(1000).Crop("scale").Quality(35).FetchFormat("webp")
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.SecureUrl.AbsoluteUri;
            }
            return null;
        }

        public async Task<string?> SaveImage(IFormFile file, string publicId, string folder)
        {
            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = publicId,
                    Folder = $"{ROOT_FOLDER}/{folder}",
                    Format = "webp",
                    Transformation = new Transformation().Width(1000).Crop("scale").Quality(35).FetchFormat("auto"),
                    Overwrite = true
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.SecureUrl.AbsoluteUri;
            }

            return null;
        }
    }
}
