using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiShop.Data.Models.Account;
using SkiShop.Data.Models.ShoppingCart;
using System.Data;

namespace SkiShop.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var user = SeedAdminUser();

            builder.HasData(user);
        }

        private ApplicationUser SeedAdminUser()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com",
                ShoppingCartId = new Guid("a8802be8-743e-45b7-963d-bc4bc494afa7")
            };

            user.PasswordHash = hasher.HashPassword(user, "qaz123");

            return user;
        }
    }
}
