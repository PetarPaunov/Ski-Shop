namespace SkiShop.Data.Models.ShoppingCart
{
    using SkiShop.Data.Models.Product;

    public class ShoppingCartProduct
    {
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
