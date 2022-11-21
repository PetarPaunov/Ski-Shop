namespace SkiShop.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Configuration for types
    /// </summary>
    public class TypeConfiguration : IEntityTypeConfiguration<Models.Product.Type>
    {
        public void Configure(EntityTypeBuilder<Models.Product.Type> builder)
        {
            var types = SeedTypes();

            builder.HasData(types);
        }

        private List<Models.Product.Type> SeedTypes()
        {
            return new List<Models.Product.Type>()
            {
                new Models.Product.Type()
                {
                    Id = new Guid("3d2d17be-461b-470f-ae18-466418081743"),
                    Name = "Snowboard"
                },
                new Models.Product.Type()
                {
                    Id = new Guid("df6c701e-4cfd-481d-ac2f-f4473fdbbe5c"),
                    Name = "Ski"
                }
            };
        }
    }
}