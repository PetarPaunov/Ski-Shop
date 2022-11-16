namespace SkiShop.Core.Contracts.ProductContracts
{
    using SkiShop.Core.Models.ProductViewModels;

    public interface IProductService
    {
        Task<IEnumerable<AllProductsViewModel>> GetFirstSixProductsAsync();
        Task<ProductViewModel> GetProductByIdAsync(string productId);
        Task<IEnumerable<AllProductsViewModel>> GetAllProductsByTypeAsync(string type);
        Task<IEnumerable<AllProductsViewModel>> GetAllProductsAsync();
        Task AddNewComment(string comment, string productId, string userId);
    }
}
