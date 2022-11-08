namespace SkiShop.Core.Contracts.ProductContracts
{
    using SkiShop.Core.Models.ProductViewModels;

    public interface IProductService
    {
        Task<IEnumerable<HomeProductViewModel>> GetFirstSixProductsAsync();
        Task<ProductsViewModel> GetProductByIdAsync(string productId);
    }
}
