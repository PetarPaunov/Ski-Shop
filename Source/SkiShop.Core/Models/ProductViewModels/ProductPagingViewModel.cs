namespace SkiShop.Core.Models.ProductViewModels
{
    public class ProductPagingViewModel
    {
        public string? Type { get; set; }
        public string? SearchTerm { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<AllProductsViewModel> Products { get; set; }
    }
}
