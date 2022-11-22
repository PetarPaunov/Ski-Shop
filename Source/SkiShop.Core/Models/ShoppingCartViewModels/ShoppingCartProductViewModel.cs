namespace SkiShop.Core.Models.ShoppingCartViewModels
{
    /// <summary>
    /// View model for extraction products in shopping cart
    /// </summary>
    public class ShoppingCartProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string TotalPrice { get; set; } = null!;
    }
}