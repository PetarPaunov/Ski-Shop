namespace SkiShop.Core.Contracts.ProductContracts
{
    using SkiShop.Core.Models.ProductViewModels;

    /// <summary>
    /// Product manipulation service
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets first six products in the databse
        /// </summary>
        /// <returns>A collection of products mapped to a view model</returns>
        Task<IEnumerable<AllProductsViewModel>> GetFirstEightProductsAsync();

        /// <summary>
        /// Gets a product from the database
        /// </summary>
        /// <param name="productId">Identifier of the product</param>
        /// <returns>A single product mapped to a view model</returns>
        Task<ProductViewModel> GetProductByIdAsync(string productId);

        /// <summary>
        /// Gets all products from the database base on conditions
        /// </summary>
        /// <param name="currentPage">Current page of the view</param>
        /// <param name="productsPerPage">Maximum products per page</param>
        /// <param name="type">Type of the product</param>
        /// <param name="searchTerm">Search term</param>
        /// <returns>View model holding the data get depending on conditions</returns>
        Task<ProductQueryViewModel> GetAllProductsAsync
            (int currentPage, int productsPerPage = 1, string? type = null, string? searchTerm = null);

        /// <summary>
        /// Add a comment to product
        /// </summary>
        /// <param name="comment">Comment description</param>
        /// <param name="productId">Identifier of the product</param>
        /// <param name="userId">Identifier of the user</param>
        Task AddNewComment(string comment, string productId, string userId);
    }
}