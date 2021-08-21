namespace MyWebApp_BikeShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyWebApp_BikeShop.Data;
    using MyWebApp_BikeShop.Models;
    using MyWebApp_BikeShop.Models.Home;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly BikeShopDbContext data;

        public HomeController(BikeShopDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            var totalBikes = this.data.Bikes.Count();

            var bikes = this.data
                .Bikes
                .OrderByDescending(b => b.Id)
                .Select(b => new BikeIndexViewModel
                {
                    Id = b.Id,
                    Brand = b.Brand,
                    Model = b.Model,
                    Year = b.Year,
                    ImageUrl = b.ImageUrl
                })
                .ToList();
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
