namespace SkiShop.Core.Contracts
{
    using SkiShop.Core.Models.ProductViewModels;

    public interface IProductService
	{
		Task<IEnumerable<AllProductsAdminViewModel>> GetAllProductsAsync();
	}
}
