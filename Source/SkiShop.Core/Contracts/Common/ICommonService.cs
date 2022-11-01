namespace SkiShop.Core.Contracts.Common
{
    using Microsoft.AspNetCore.Http;

    public interface ICommonService
	{
        string UploadedFile(IFormFile frontImage);

    }
}
