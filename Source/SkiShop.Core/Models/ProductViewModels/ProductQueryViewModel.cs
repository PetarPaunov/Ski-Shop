namespace SkiShop.Core.Models.ProductViewModels
{
	public class ProductQueryViewModel
	{
        public int TotalProductsCount { get; set; }

        public IEnumerable<AllProductsViewModel> Products { get; set; } = new List<AllProductsViewModel>();
    }
}