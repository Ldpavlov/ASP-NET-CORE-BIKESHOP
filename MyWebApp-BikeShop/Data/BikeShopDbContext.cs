namespace MyWebApp_BikeShop.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MyWebApp_BikeShop.Data.Models;

    public class BikeShopDbContext : IdentityDbContext
    {
        public BikeShopDbContext(DbContextOptions<BikeShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bike> Bikes { get; init; }

        public DbSet<Category> Categories { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Bike>()
                    .HasOne(c => c.Category)
                    .WithMany(b => b.Bikes)
                    .HasForeignKey(c => c.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
