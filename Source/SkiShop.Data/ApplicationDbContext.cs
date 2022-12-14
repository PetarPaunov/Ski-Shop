namespace SkiShop.Data
{
    using SkiShop.Data.Models.Account;
    using SkiShop.Data.Configuration;
    using SkiShop.Data.Models.Product;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using SkiShop.Data.Models.ShoppingCart;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }

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

            builder.Entity<ProductComment>()
                .HasKey(x => new { x.ProductId, x.CommentId });

            builder.Entity<UserComment>()
                .HasKey(x => new { x.ApplicationUserId, x.CommentId });

            builder.Entity<ShoppingCart>()
                .HasOne(x => x.User)
                .WithOne(x => x.ShoppingCart)
                .HasForeignKey<ApplicationUser>(x => x.ShoppingCartId)
                .OnDelete(DeleteBehavior.Restrict);


            // builder.ApplyConfiguration<Type>(new TypeConfiguration());
            // builder.ApplyConfiguration<Brand>(new BrandConfiguration());
            // builder.ApplyConfiguration<Model>(new ModelConfiguration());
            // builder.ApplyConfiguration<ShoppingCart>(new ShoppingCartConfiguration());
            // builder.ApplyConfiguration<ApplicationUser>(new UserConfiguration());
            // builder.ApplyConfiguration<IdentityRole>(new RoleConfiguration());
            // builder.ApplyConfiguration<IdentityUserRole<string>>(new UserToRoleConfiguration());


            base.OnModelCreating(builder);
        }
    }
}