namespace MyWebApp_BikeShop.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using MyWebApp_BikeShop.Data;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using MyWebApp_BikeShop.Data.Models;
    using System;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    using static WebConstants;


    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedCategories(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<BikeShopDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<BikeShopDbContext>();

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

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@pry.com";
                    const string adminPassword = "admin766";

                    var user = new IdentityUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
