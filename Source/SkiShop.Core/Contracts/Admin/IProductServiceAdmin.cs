namespace SkiShop.Core.Contracts
{
    using SkiShop.Core.Models.BrandModels;
    using SkiShop.Core.Models.ModelViewModels;
    using SkiShop.Core.Models.ProductViewModels;
    using SkiShop.Core.Models.TypeModels;

    public interface IProductServiceAdmin
	{
        Task<IEnumerable<TypeViewModel>> GetAllTypesAsync();
        Task<IEnumerable<ModelViewModel>> GetAllModelsAsync();
        Task<IEnumerable<BrandViewModel>> GetAllBrandsAsync();
        Task AddNewProductAsync(AddProductViewModel model);
        Task<IEnumerable<AllProductsViewModel>> GetAllProductsAsync();
        Task<IEnumerable<AllProductsViewModel>> GetAllDeletedProductsAsync();
        Task ReturnDeletedProduct(string productId, int quantity);
        Task EditAsync(EditProductViewModel model);
		Task<EditProductViewModel> GetForEditAsync(string id);
		Task DeleteSingleProductAsync(string id);
		Task DeleteProduct(string id);
        Task AddOthers(string keyWord, string name);
    }
}
