using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiShop.Data.Models.ShoppingCart;

namespace SkiShop.Data.Configuration
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            var shoppingCart = SeedShoppingCart();

            builder.HasData(shoppingCart);
        }

        private ShoppingCart SeedShoppingCart()
        {
            var cart = new ShoppingCart()
            {
                Id = new Guid("a8802be8-743e-45b7-963d-bc4bc494afa7"),
            };

            return cart;
        }
    }
}
