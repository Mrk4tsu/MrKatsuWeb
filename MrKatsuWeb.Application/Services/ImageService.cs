using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using MrKatsuWeb.Application.Interfaces.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public MemoryStream ConvertToWebP(IFormFile file)
        {
            const int maxFileSize = 2 * 1024 * 1024;
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is invalid or empty");
            }

            var memoryStream = new MemoryStream();

            using (var image = new MagickImage(file.OpenReadStream()))
            {
                if (file.Length > maxFileSize)
                    image.Quality = 30;
                else
                    image.Quality = 60;
                image.Format = MagickFormat.WebP;
                image.Write(memoryStream);
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        public async Task<string?> SaveImage(IFormFile file)
        {
            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                using (var convertedStream = ConvertToWebP(file))
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, convertedStream),
                        Overwrite = true
                    };
                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    return uploadResult.SecureUrl.AbsoluteUri;
                }                               
            }
            return null;
        }

        public async Task<string?> SaveImage(IFormFile file, string publicId, string folder)
        {
            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                using (var convertedStream = ConvertToWebP(file))
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, convertedStream),
                        PublicId = publicId,
                        Folder = $"{ROOT_FOLDER}/{folder}",
                        Overwrite = true
                    };
                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    return uploadResult.SecureUrl.AbsoluteUri;
                }
            }
            return null;
        }
    }
}
