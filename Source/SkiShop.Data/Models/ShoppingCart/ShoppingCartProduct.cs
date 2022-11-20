namespace SkiShop.Data.Models.ShoppingCart
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SkiShop.Data.Models.Product;

    public class ShoppingCartProduct
    {
        public Guid Id { get; set; }

        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}