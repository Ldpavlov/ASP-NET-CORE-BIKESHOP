namespace MyWebApp_BikeShop.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using MyWebApp_BikeShop.Data;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using MyWebApp_BikeShop.Data.Models;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<BikeShopDbContext>();

            data.Database.Migrate();

            SeedCategories(data);
            
            return app;        
        }

        private static void SeedCategories(BikeShopDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category {Name = "Road"},
                new Category {Name = "Mountain"},
                new Category {Name = "Fixie"},
                new Category {Name = "BMX"},
                new Category {Name = "Dirt"},
                new Category {Name = "Downhill"},
                new Category {Name = "Enduro"},
                new Category {Name = "Crosscountry"},
            });

            data.SaveChanges();
        }
                
    }
}
