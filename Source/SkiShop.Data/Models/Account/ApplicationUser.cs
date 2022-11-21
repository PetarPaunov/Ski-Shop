namespace SkiShop.Data.Models.Account
{
    using SkiShop.Data.Models.Product;
    using Microsoft.AspNetCore.Identity;
    using SkiShop.Data.Models.ShoppingCart;

    /// <summary>
    /// IdentityUser Extension
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Comments = new HashSet<UserComment>();
        }

        /// <summary>
        /// Id of the user's shopping cart
        /// </summary>
        public Guid ShoppingCartId { get; set; }

        /// <summary>
        /// Reference to the actual shopping cart
        /// </summary>
        public ShoppingCart ShoppingCart { get; set; } = null!;

        /// <summary>
        /// A collection of comments posted by a user
        /// </summary>
        public ICollection<UserComment> Comments { get; set; }
    }
}