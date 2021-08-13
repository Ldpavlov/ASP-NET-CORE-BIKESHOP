namespace MyWebApp_BikeShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyWebApp_BikeShop.Data;
    using MyWebApp_BikeShop.Data.Models;
    using MyWebApp_BikeShop.Infrastructure;
    using MyWebApp_BikeShop.Models.Sellers;
    using System.Linq;

    public class SellersController : Controller
    {
        private readonly BikeShopDbContext data;

        public SellersController(BikeShopDbContext data) =>
            this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeSellerFormModel seller)
        {
            var userId = this.User.GetId();

            var userIsAseller = this.data
                .Sellers
                .Any(s => s.UserId == userId);

            if (userIsAseller)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(seller);
            }

            var sellerData = new Seller
            {
                Name = seller.Name,
                PhoneNumber = seller.PhoneNumber,
                UserId = userId
            };

            this.data.Sellers.Add(sellerData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Bike");
        }
    }
}
