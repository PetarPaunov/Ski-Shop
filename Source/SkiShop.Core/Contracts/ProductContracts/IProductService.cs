namespace SkiShop.Core.Contracts.ProductContracts
{
    using SkiShop.Core.Models.ProductViewModels;

    public interface IProductService
    {
        Task<IEnumerable<AllProductsViewModel>> GetFirstSixProductsAsync();
        Task<ProductViewModel> GetProductByIdAsync(string productId);
        Task<ProductQueryViewModel> GetAllProductsAsync
            (int currentPage, int housesPerPage = 1, string? type = null, string? searchTerm = null);
        Task AddNewComment(string comment, string productId, string userId);
    }
}
