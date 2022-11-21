﻿namespace SkiShop.Core.Contracts.Common
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Common services
    /// </summary>
    public interface ICommonService
	{
        /// <summary>
        /// Uploads image to a cloud service
        /// </summary>
        /// <param name="imageFile">Uploaded image</param>
        /// <returns></returns>
        Task<string> UploadeImage(IFormFile imageFile);
    }
}