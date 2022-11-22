namespace SkiShop.Core.Models.ProductViewModels
{
    /// <summary>
    /// View model for extracting products from the database (User)
    /// </summary>
    public class AllProductsViewModel
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}