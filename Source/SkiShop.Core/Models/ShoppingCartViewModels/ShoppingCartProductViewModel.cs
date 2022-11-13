namespace SkiShop.Core.Models.ShoppingCartViewModels
{
    public class ShoppingCartProductViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string TotalPrice { get; set; }
    }
}
