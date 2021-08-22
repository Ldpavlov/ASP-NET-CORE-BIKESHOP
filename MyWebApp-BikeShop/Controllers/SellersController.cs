namespace MyWebApp_BikeShop.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyWebApp_BikeShop.Data;
    using MyWebApp_BikeShop.Data.Models;
    using MyWebApp_BikeShop.Infrastructure;
    using MyWebApp_BikeShop.Models.Sellers;
    using MyWebApp_BikeShop.Services.Sellers;
    using System.Linq;

    public class SellersController : Controller
    {
        private readonly BikeShopDbContext data;
        private ISellersService sellersService;
        private readonly IMapper mapper;
        private readonly IConfigurationProvider selectionMapper;

        public SellersController(BikeShopDbContext data, ISellersService sellersService, IMapper mapper)
        {
            this.data = data;
            this.sellersService = sellersService;
            this.mapper = mapper;
            this.selectionMapper = mapper.ConfigurationProvider;
        }

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeSellerFormModel seller)
        {
            var userId = this.User.GetId();

            var userIsAseller = sellersService.IsValidSeller(userId);

            if (userIsAseller)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(seller);
            }   

            var serviceModel = 
                mapper.Map<BecomeSellerFormModel, BecomeSellerServiceModel>(seller);

            serviceModel.UserId = this.User.GetId();

            sellersService.Become(serviceModel);

            return RedirectToAction("All", "Bike");
        }
    }
}
