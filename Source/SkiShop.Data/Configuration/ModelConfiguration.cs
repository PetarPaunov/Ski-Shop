namespace SkiShop.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Data.Models.ProductCommon;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            var models = SeedModels();

            builder.HasData(models);
        }

        private List<Model> SeedModels()
        {
            return new List<Model>()
            {
                new Model()
                {
                    Id = new Guid("4138f243-22c4-4ed5-a7e0-d85676b7add8"),
                    Name = "ESSENTIAL SERVICE"
                },
                new Model()
                {
                    Id = new Guid("a5c6ad42-4a28-4c7b-ae32-8e51b6344076"),
                    Name = "Riders Choice"
                },
                new Model()
                {
                    Id = new Guid("e1edb175-19c6-49c0-a6f3-7c27293023c1"),
                    Name = "GWO"
                },
                new Model()
                {
                    Id = new Guid("10fd1d2d-7d59-440e-976b-b91b194c797f"),
                    Name = "ORCA"
                },
                new Model()
                {
                    Id = new Guid("6f44da8f-b042-4ff3-9e7e-13a26ddb770d"),
                    Name = "WUNDERSTICK"
                },
                new Model()
                {
                    Id = new Guid("df762cb0-95a2-4acf-97ea-281141dee642"),
                    Name = "Ufo"
                },
                new Model()
                {
                    Id = new Guid("b5e5d16e-8a91-45fe-902c-3fef88420487"),
                    Name = "LIBSTICK"
                },
                new Model()
                {
                    Id = new Guid("ec151db3-c524-4354-815e-d42c08d06bc7"),
                    Name = "WRECKCREATE"
                }
            };
        }
    }
}