namespace SkiShop.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Ski> Skis { get; set; }
        public DbSet<SkiType> SkiTypes { get; set; }
        public DbSet<Snowboard> Snowboards { get; set; }
        public DbSet<SnowboardBinding> SnowboardBindings { get; set; }
        public DbSet<SnowboardBoot> SnowboardBoots { get; set; }

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

            builder.Entity<Ski>()
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Entity<SkiType>()
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Entity<Snowboard>()
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Entity<SnowboardBinding>()
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Entity<SnowboardBoot>()
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);


            base.OnModelCreating(builder);
        }
    }
}