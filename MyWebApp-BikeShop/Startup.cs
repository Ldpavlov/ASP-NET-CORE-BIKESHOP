namespace MyWebApp_BikeShop
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MyWebApp_BikeShop.Data;
    using MyWebApp_BikeShop.Infrastructure;
    using MyWebApp_BikeShop.Services.Bikes;
    using MyWebApp_BikeShop.Services.Buyers;
    using MyWebApp_BikeShop.Services.Sellers;

    public class Startup
    {
        public Startup(IConfiguration configuration) 
            => Configuration = configuration;
        

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<BikeShopDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<IdentityUser>(options => 
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BikeShopDbContext>();
            services.AddAutoMapper(typeof(Startup));

            services              
                    .AddControllersWithViews(options =>
                    {
                        options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                    });
            services.AddTransient<IBikeService, BikeService>();
            services.AddTransient<ISellersService, SellerService>();
            services.AddTransient<IBuyerService, BuyerService>();
        }
      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");               
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });            
        }
    }
}
