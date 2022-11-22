namespace SkiShop.Core.Models.ProductViewModels
{
    /// <summary>
    /// View model for extracting products from the database (Administration)
    /// </summary>
    public class AllProductsAdminViewModel
	{
		public string Id { get; set; } = null!;
		public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Price { get; set; } = null!;
        public int Quantity { get; set; }
		public string ImageUrl { get; set; } = null!;
    }
}