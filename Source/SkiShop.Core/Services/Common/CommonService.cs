namespace SkiShop.Core.Services.Common
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using SkiShop.Core.Contracts.Common;

    public class CommonService : ICommonService
    {
        private readonly Cloudinary cloudinary;

        public CommonService(IWebHostEnvironment _webHostEnvironment, Cloudinary _cloudinary)
        {
            cloudinary = _cloudinary;
        }

        public async Task<string> UploadeImage(IFormFile imageFile)
        {
            using var stream = imageFile.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageFile.FileName, stream)
            };

            var result = await cloudinary.UploadAsync(uploadParams);

            var imageUrl = result.Url.ToString();

            return imageUrl;
        }
    }
}
