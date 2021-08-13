namespace MyWebApp_BikeShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyWebApp_BikeShop.Data;
    using MyWebApp_BikeShop.Data.Models;
    using MyWebApp_BikeShop.Infrastructure;
    using MyWebApp_BikeShop.Models;
    using MyWebApp_BikeShop.Models.Bikes;
    using System.Collections.Generic;
    using System.Linq;

    public class BikeController : Controller
    {
        private readonly BikeShopDbContext data;

        public BikeController(BikeShopDbContext data)
            => this.data = data;

        public IActionResult All([FromQuery] AllBikesViewModel query)
        {
            var bikesQuery = this.data.Bikes.AsQueryable();

            if (!string.IsNullOrEmpty(query.Brand))
            {
                bikesQuery = bikesQuery.Where(b => b.Brand == query.Brand);
            }

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                bikesQuery = bikesQuery.Where(b =>
                (b.Brand + " " + b.Model).ToLower().Contains(query.SearchTerm.ToLower()) ||
                b.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            bikesQuery = query.Sorting switch
            {
                BikeSort.Year => bikesQuery.OrderByDescending(b => b.Year),
                BikeSort.BrandModel => bikesQuery.OrderBy(b => b.Brand).ThenBy(b => b.Model),
                BikeSort.CreatedOn or _ => bikesQuery.OrderByDescending(b => b.Id)
            };

            var totalBikes = bikesQuery.Count();

            var bikes = bikesQuery
                .Skip((query.CurrentPage - 1) * AllBikesViewModel.BikesPerPage)
                .Take(AllBikesViewModel.BikesPerPage)
                .Select(b => new BikeListingViewModel
                {
                    Id = b.Id,
                    Brand = b.Brand,
                    Model = b.Model,
                    Year = b.Year,
                    ImageUrl = b.ImageUrl,
                    Category = b.Category.Name
                })
                .ToList();

            var bikeBrands = this.data
                .Bikes
                .Select(b => b.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

            query.TotalBikesCount = totalBikes;
            query.Brands = bikeBrands;
            query.Bikes = bikes;

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

        public IActionResult Detail()
        {

            return View();
        }

    }

}
