namespace SkiShop.Data.Models.ShoppingCart
{
    using SkiShop.Data.Models.Product;

    public class ShoppingCartProduct
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id of the shopping cart 
        /// </summary>
        public Guid ShoppingCartId { get; set; }

        /// <summary>
        /// Reference to the actual shopping cart
        /// </summary>
        public ShoppingCart ShoppingCart { get; set; } = null!;

        /// <summary>
        /// Id of the product 
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Reference to the actual product
        /// </summary>
        public Product Product { get; set; } = null!;

        /// <summary>
        /// Quantity of a product
        /// </summary>
        public int Quantity { get; set; }
    }
}