namespace SkiShop.Core.Contracts
{
    using SkiShop.Core.Models.TypeModels;
    using SkiShop.Core.Models.BrandModels;
    using SkiShop.Core.Models.ModelViewModels;
    using SkiShop.Core.Models.ProductViewModels;

    /// <summary>
    /// Administrator services for the product
    /// </summary>
    public interface IProductServiceAdmin
	{
        /// <summary>
        /// Gets all product types from the database
        /// </summary>
        /// <returns>A collection of types mapped to a view model</returns>
        Task<IEnumerable<TypeViewModel>> GetAllTypesAsync();

        /// <summary>
        /// Gets all product models from the database
        /// </summary>
        /// <returns>A collection of models mapped to a view model</returns>
        Task<IEnumerable<ModelViewModel>> GetAllModelsAsync();

        /// <summary>
        /// Gets all product banrds from the database
        /// </summary>
        /// <returns>A collection of brands mapped to a view model</returns>
        Task<IEnumerable<BrandViewModel>> GetAllBrandsAsync();

        /// <summary>
        /// Add a new product to the database
        /// </summary>
        /// <param name="model">Object with required product data</param>
        Task AddNewProductAsync(AddProductViewModel model);

        /// <summary>
        /// Gets all products from the database
        /// </summary>
        /// <returns>A collection of products mapped to a view model</returns>
        Task<IEnumerable<AllProductsAdminViewModel>> GetAllProductsAsync();

        /// <summary>
        /// Gets all products with (IsDeleted flag = ture) from the database
        /// </summary>
        /// <returns>A collection of products mapped to a view model</returns>
        Task<IEnumerable<AllProductsAdminViewModel>> GetAllDeletedProductsAsync();

        /// <summary>
        /// Sets IsDeleted flag to false and add a new quantity for the product
        /// </summary>
        /// <param name="productId">The id of the deleted product</param>
        /// <param name="quantity">Quantity for the product</param>
        Task ReturnDeletedProductAsync(string productId, int quantity);

        /// <summary>
        /// Edit existing product
        /// </summary>
        /// <param name="model">Contains the product data for edit</param>
        Task EditAsync(EditProductViewModel model);

        /// <summary>
        /// Get existing product from database for editing
        /// </summary>
        /// <param name="id">Identifier of the product</param>
        /// <returns>A product mapped to a view model</returns>
		Task<EditProductViewModel> GetForEditAsync(string id);

        /// <summary>
        /// Remove one from the product quantity
        /// </summary>
        /// <param name="id">Identifire of the product</param>
		Task DeleteSingleProductAsync(string id);

        /// <summary>
        /// Sets the IsDeleted flag to true (product is deleted)
        /// </summary>
        /// <param name="id">Identifier of the product</param>
		Task DeleteProductAsync(string id);

        /// <summary>
        /// Add a new Type, Brand or Model to database
        /// </summary>
        /// <param name="keyWord">Can be Type, Brand, Model</param>
        /// <param name="name">Name of the entity</param>
        Task AddOthersAsync(string keyWord, string name);
    }
}