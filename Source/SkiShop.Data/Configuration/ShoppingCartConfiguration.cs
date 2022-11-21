namespace SkiShop.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Data.Models.ShoppingCart;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Configuration for shopping cart
    /// </summary>
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
                UserId = "dea12856-c198-4129-b3f3-b893d8395082"
            };

            return cart;
        }
    }
}