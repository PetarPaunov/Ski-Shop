namespace SkiShop.Core.Contracts.Common
{
    using Microsoft.AspNetCore.Http;

    public interface ICommonService
	{
        Task<string> UploadeImage(IFormFile imageFile);
    }
}
