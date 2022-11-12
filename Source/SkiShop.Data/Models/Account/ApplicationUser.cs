namespace SkiShop.Data.Models.Account
{
    using Microsoft.AspNetCore.Identity;
    using SkiShop.Data.Models.Product;
    using SkiShop.Data.Models.ShoppingCart;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Comments = new HashSet<UserComment>();
        }

        public Guid ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; } 

        public ICollection<UserComment> Comments { get; set; }
    }
}