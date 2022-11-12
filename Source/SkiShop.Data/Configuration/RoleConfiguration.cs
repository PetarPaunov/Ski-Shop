namespace SkiShop.Data.Configuration
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            var roles = SeedRoles();

            builder.HasData(roles);

        }

        private List<IdentityRole> SeedRoles()
        {
            var roles = new List<IdentityRole>();

            var adminRole = new IdentityRole()
            {
                Id = "6ae05bb5-2a6f-418d-b860-5b912480f1bc",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = "037a686e-cf2a-4612-acaa-0452b80bdf6e"
            };

            roles.Add(adminRole);

            var userRole = new IdentityRole()
            {
                Id = "030b13b9-535c-48f4-9da9-6799f590dcff",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "70fb635a-b79a-4b22-bcf8-967ea79aee74"
            };

            roles.Add(userRole);

            return roles;
        }
    }
}