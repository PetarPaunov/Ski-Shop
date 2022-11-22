namespace SkiShop.Core.Models.ProductViewModels
{
    /// <summary>
    /// View model for paging and searching functionality
    /// </summary>
    public class ProductPagingViewModel
    {
        public int ProductsPerPage { get; set; } = 12;
        public int CurrentPage { get; set; } = 1;
        public string? Type { get; set; }
        public string? SearchTerm { get; set; }
        public int TotalProductsCount { get; set; }
        public IEnumerable<AllProductsViewModel> Products { get; set; } = null!;
    }
}