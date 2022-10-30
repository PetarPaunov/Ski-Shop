namespace SkiShop.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TypeConfiguration : IEntityTypeConfiguration<Models.ProductCommon.Type>
    {
        public void Configure(EntityTypeBuilder<Models.ProductCommon.Type> builder)
        {
            var types = SeedTypes();

            builder.HasData(types);
        }

        private List<Models.ProductCommon.Type> SeedTypes()
        {
            return new List<Models.ProductCommon.Type>()
            {
                new Models.ProductCommon.Type()
                {
                    Id = new Guid("3d2d17be-461b-470f-ae18-466418081743"),
                    Name = "Snowboard"
                },
                new Models.ProductCommon.Type()
                {
                    Id = new Guid("df6c701e-4cfd-481d-ac2f-f4473fdbbe5c"),
                    Name = "Ski"
                }
            };
        }
    }
}
