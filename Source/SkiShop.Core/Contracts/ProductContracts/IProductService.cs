namespace SkiShop.Core.Contracts.ProductContracts
{
    using SkiShop.Core.Models.ProductViewModels;
    using SkiShop.Data.Models.Product;

    public interface IProductService
    {
        Task<IEnumerable<HomeProductViewModel>> GetFirstSixProductsAsync();
        Task<ProductViewModel> GetProductByIdAsync(string productId);
        Task AddNewComment(string comment, string productId, string userId);
    }
}
