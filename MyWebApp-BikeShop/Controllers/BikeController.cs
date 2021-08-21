namespace MyWebApp_BikeShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using MyWebApp_BikeShop.Data;
    using MyWebApp_BikeShop.Data.Models;
    using MyWebApp_BikeShop.Infrastructure;
    using MyWebApp_BikeShop.Models;
    using MyWebApp_BikeShop.Models.Bikes;
    using MyWebApp_BikeShop.Services.Bikes;
    using System.Collections.Generic;
    using System.Linq;

    public class BikeController : Controller
    {
        private readonly BikeShopDbContext data;
        private readonly IBikeService bikes;          

        public BikeController(IBikeService bikes, BikeShopDbContext data)
        {
            this.data = data;
            this.bikes = bikes;
        }

        public IActionResult All([FromQuery] AllBikesViewModel query)
        {
            var bikes = this.bikes.All(
                query.Brand,
                query.SearchTerm,
                query.CurrentPage,
                query.BikesPerPage);

            var bikeBrands = this.bikes.AllBikeBrands();

            query.TotalBikesCount = bikes.TotalBikesCount;
            query.Brands = bikeBrands;
            query.Bikes = bikes.Bikes.Select(b => new BikeListingViewModel
            {
                Id = b.Id,
                Brand = b.Brand,
                Model = b.Model,
                Category = b.Category,
                ImageUrl = b.ImageUrl,
                Year = b.Year
            });

            return View(query);
        }        
           

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsSeller())
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            return View(new AddBikeFormModel
            {
                Categories = this.GetBikeCategories()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddBikeFormModel bike)
        {
            var sellerId = this.data
                .Sellers
                .Where(s => s.UserId == this.User.GetId())
                .Select(s => s.Id)
                .FirstOrDefault();

            if (sellerId == 0)
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            if (!this.data.Categories.Any(c => c.Id == bike.CategoryId))
            {
                this.ModelState.AddModelError(nameof(bike.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                bike.Categories = this.GetBikeCategories();
                return View(bike);
            }

            var bikeData = new Bike
            {
                Brand = bike.Brand,
                Model = bike.Model,
                Description = bike.Description,
                ImageUrl = bike.ImageUrl,
                CategoryId = bike.CategoryId,
                Year = bike.Year,
                SellerId = sellerId
            };

            this.data.Bikes.Add(bikeData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsSeller() =>
            this.data
              .Sellers
              .Any(s => s.UserId == this.User.GetId());

        private IEnumerable<BikeCategoryViewModel> GetBikeCategories()
            => this.data
                .Categories
                .Select(c => new BikeCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
            .ToList();

    }

}
