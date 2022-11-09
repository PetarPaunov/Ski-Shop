namespace SkiShop.Data.Models.Account
{
    using Microsoft.AspNetCore.Identity;
    using SkiShop.Data.Models.Product;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Comments = new HashSet<UserComment>();
        }

        public ICollection<UserComment> Comments { get; set; }
    }
}