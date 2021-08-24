namespace MyWebApp_BikeShop.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyWebApp_BikeShop.Data;
    using MyWebApp_BikeShop.Infrastructure;
    using MyWebApp_BikeShop.Models.Buyer;
    using MyWebApp_BikeShop.Services.Buyers;
    using MyWebApp_BikeShop.Services.Buyers.Model;

    public class BuyersController : Controller
    {
        private readonly BikeShopDbContext data;
        private IBuyerService buyerService;
        private readonly IMapper mapper;

        public BuyersController(BikeShopDbContext data, IBuyerService buyerService, IMapper mapper)
        {
            this.data = data;
            this.buyerService = buyerService;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeABuyerFormModel model)
        {            
            var userId = this.User.GetId();

            var userIsABuyer = buyerService.IsBuyer(userId);

            if (userIsABuyer)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var serviceModel =
                mapper.Map<BecomeABuyerFormModel, BecomeABuyerServiceModel>(model);

            serviceModel.UserId = this.User.GetId();

            buyerService.Become(serviceModel);

            return RedirectToAction("All", "Bike");
        }
    }
}
