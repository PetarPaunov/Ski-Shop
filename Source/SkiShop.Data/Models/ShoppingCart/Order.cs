namespace SkiShop.Data.Models.ShoppingCart
{
    using SkiShop.Data.Models.Account;
    using SkiShop.Data.Models.Product;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// A order of a user
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id of the user 
        /// </summary>
        public string ApplicationUserId { get; set; } = null!;

        /// <summary>
        /// Reference to the actual user
        /// </summary>
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        /// <summary>
        /// Id of the product 
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Reference to the actual product
        /// </summary>
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        /// <summary>
        /// Quantity of the product
        /// </summary>
        public int Quantity { get; set; }
    }
}