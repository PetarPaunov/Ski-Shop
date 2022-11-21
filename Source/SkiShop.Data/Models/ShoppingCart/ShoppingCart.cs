namespace SkiShop.Data.Models.ShoppingCart
{
    using SkiShop.Data.Models.Account;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// A shopping cart that every newly registered user will have
    /// </summary>
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            ShoppingCartProducts = new HashSet<ShoppingCartProduct>();
        }

        /// <summary>
        /// Unique identifier
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Id of the user 
        /// </summary>
        public string UserId { get; set; } = null!;

        /// <summary>
        /// Reference to the actual user
        /// </summary>
        public ApplicationUser User { get; set; } = null!;

        /// <summary>
        /// A collection of products in the shopping cart
        /// </summary>
        public ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; }
    }
}