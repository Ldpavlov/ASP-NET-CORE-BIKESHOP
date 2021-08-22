namespace MyWebApp_BikeShop.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using MyWebApp_BikeShop.Data;
    using MyWebApp_BikeShop.Models;
    using MyWebApp_BikeShop.Models.Bikes;
    using MyWebApp_BikeShop.Models.Home;
    using MyWebApp_BikeShop.Services.Bikes;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly BikeShopDbContext data;
        private IBikeService service;
        private readonly IMapper mapper;
        private readonly IConfigurationProvider selectionMapper;

        public HomeController(BikeShopDbContext data, IBikeService service, IMapper mapper)
        {
            this.data = data;
            this.service = service;
            this.mapper = mapper;
            this.selectionMapper = mapper.ConfigurationProvider;
        }           

        public IActionResult Index()
        {
            var totalBikes = service.TotalBikes();

            var bikes = service.AllBikes();

            return View(new IndexViewModel
            {
                TotalBikes = totalBikes,
                Bikes = bikes.AsQueryable().ProjectTo<BikeListingViewModel>(this.selectionMapper).ToList()
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
