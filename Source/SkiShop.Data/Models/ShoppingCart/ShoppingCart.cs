using SkiShop.Data.Models.Account;

namespace SkiShop.Data.Models.ShoppingCart
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            ShoppingCartProducts = new HashSet<ShoppingCartProduct>();
        }

        public Guid Id { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; }
    }
}
