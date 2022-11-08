namespace SkiShop.Data
{
    using SkiShop.Data.Models.Account;
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Data.Models.Product;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using SkiShop.Data.Configuration;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<ProductCommet> ProductCommets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Brand>()
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Entity<Comment>()
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Entity<Model>()
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Entity<Product>()
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Entity<Type>()
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Entity<ProductCommet>()
                .HasKey(x => new { x.ProductId, x.CommentId });

            builder.ApplyConfiguration<Type>(new TypeConfiguration());
            builder.ApplyConfiguration<Brand>(new BrandConfiguration());
            builder.ApplyConfiguration<Model>(new ModelConfiguration());


            base.OnModelCreating(builder);
        }
    }
}