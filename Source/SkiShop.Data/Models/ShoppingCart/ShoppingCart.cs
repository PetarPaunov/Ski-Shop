namespace SkiShop.Data.Models.ShoppingCart
{
    using SkiShop.Data.Models.Account;
    using System.ComponentModel.DataAnnotations;

    public class ShoppingCart
    {
        public ShoppingCart()
        {
            ShoppingCartProducts = new HashSet<ShoppingCartProduct>();
        }

        [Key]
        public Guid Id { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; }
    }
}