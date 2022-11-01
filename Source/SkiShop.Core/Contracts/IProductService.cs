namespace SkiShop.Core.Contracts
{
    using SkiShop.Core.Models.ProductViewModels;

    public interface IProductService
	{
		Task<IEnumerable<ProductsViewModel>> GetAllProductsAsync();
		Task EditAsync(EditProductViewModel model);
		Task<EditProductViewModel> GetForEditAsync(string id);
		Task DeleteSingleProductAsync(string id);
		Task DeleteProduct(string id);
	}
}
