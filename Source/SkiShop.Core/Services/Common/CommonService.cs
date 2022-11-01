namespace SkiShop.Core.Services.Common
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using SkiShop.Core.Contracts.Common;

    public class CommonService : ICommonService
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public CommonService(IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
        }

        public string UploadedFile(IFormFile frontImage)
        {
            string uniqueFileName = null;

            if (frontImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "product-images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + frontImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    frontImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
