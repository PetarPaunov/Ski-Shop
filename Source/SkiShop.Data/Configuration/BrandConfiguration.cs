namespace SkiShop.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Data.Models.Product;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            var brands = SeedBrands();

            builder.HasData(brands);
        }

        private List<Brand> SeedBrands()
        {
            return new List<Brand>()
            {
                new Brand()
                {
                    Id = new Guid("dfbe4dc4-ce79-4eb2-a962-d3d2be272d64"),
                    Name = "LibTech"
                },
                new Brand()
                {
                    Id = new Guid("15408988-ea25-4b1b-afad-cad51fc04e1a"),
                    Name = "Gnu"
                },
                new Brand()
                {
                    Id = new Guid("356d5dd6-9564-49d3-8d93-b27f97452cec"),
                    Name = "Drake"
                },
                new Brand()
                {
                    Id = new Guid("b824c2a8-6161-40bc-aa67-712eb5247010"),
                    Name = "BontRager"
                },
                new Brand()
                {
                    Id = new Guid("caa4d197-4728-4d7a-80ef-1f31d008ba4d"),
                    Name = "FiveTen"
                },
                new Brand()
                {
                    Id = new Guid("f1e3e696-f601-4a5b-a067-5e54bb4f639c"),
                    Name = "NorthWave"
                },
                new Brand()
                {
                    Id = new Guid("0ebc966b-66e7-4790-bdc3-35a1697db071"),
                    Name = "Polaroid"
                },
                new Brand()
                {
                    Id = new Guid("cd9aaf69-1f97-4598-847d-be3c5949f12a"),
                    Name = "Smith"
                },
                new Brand()
                {
                    Id = new Guid("0b5058b3-4a2e-47f0-affe-ae4cc20d52c4"),
                    Name = "Trek"
                }
            };
        }
    }
}
