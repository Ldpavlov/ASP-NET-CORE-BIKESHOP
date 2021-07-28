namespace MyWebApp_BikeShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyWebApp_BikeShop.Data;
    using MyWebApp_BikeShop.Data.Models;
    using MyWebApp_BikeShop.Models.Bikes;
    using System.Collections.Generic;
    using System.Linq;

    public class BikeController : Controller
    {
        private readonly BikeShopDbContext data;

        public BikeController(BikeShopDbContext data)
            => this.data = data;

        public IActionResult Add() => View(new AddBikeFormModel
            {
                Categories = this.GetBikeCategories()
            });

        [HttpPost]
        public IActionResult Add(AddBikeFormModel bike)
        {
            if(!this.data.Categories.Any(c => c.Id == bike.CategoryId))
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
                CategoryId = bike.CategoryId
            };

            this.data.Bikes.Add(bikeData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

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
