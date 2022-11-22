namespace SkiShop.Core.Services.Common
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using SkiShop.Core.Contracts.Common;

    /// <summary>
    /// Common services
    /// </summary>
    public class CommonService : ICommonService
    {
        private readonly Cloudinary cloudinary;

        public CommonService(Cloudinary _cloudinary)
        {
            cloudinary = _cloudinary;
        }

        /// <summary>
        /// Uploads image to a cloud service
        /// </summary>
        /// <param name="imageFile">Uploaded image</param>
        /// <returns>Image URL</returns>
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