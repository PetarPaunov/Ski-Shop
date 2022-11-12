using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SkiShop.Data.Configuration
{
    public class UserToRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            var userToRole = SeedUserToRole();

            builder.HasData(userToRole);
        }

        private IdentityUserRole<string> SeedUserToRole()
        {
            var result = new IdentityUserRole<string>()
            {
                RoleId = "6ae05bb5-2a6f-418d-b860-5b912480f1bc",
                UserId = "dea12856-c198-4129-b3f3-b893d8395082"
            };

            return result;
        }
    }
}