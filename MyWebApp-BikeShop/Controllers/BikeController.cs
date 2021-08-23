namespace MyWebApp_BikeShop.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyWebApp_BikeShop.Data;
    using MyWebApp_BikeShop.Infrastructure;
    using MyWebApp_BikeShop.Models;
    using MyWebApp_BikeShop.Models.Bikes;
    using MyWebApp_BikeShop.Services.Bikes;
    using MyWebApp_BikeShop.Services.Bikes.Models;
    using MyWebApp_BikeShop.Services.Buyers;
    using MyWebApp_BikeShop.Services.Sellers;
    using System.Linq;

    public class BikeController : Controller
    {
        private readonly BikeShopDbContext data;
        private IBikeService bikeService;
        private IBuyerService buyerService;
        private ISellersService sellerService;
        private readonly IMapper mapper;
        private readonly IConfigurationProvider selectionMapper;

        public BikeController(BikeShopDbContext data, IBikeService bikeService, IMapper mapper, ISellersService sellersService, IBuyerService buyerService)
        {
            this.data = data;
            this.bikeService = bikeService;
            this.mapper = mapper;
            this.selectionMapper = mapper.ConfigurationProvider;
            this.sellerService = sellersService;
            this.buyerService = buyerService;
        }

        public IActionResult All([FromQuery] AllBikesViewModel query)
        {
            ViewBag.UserId = this.User.GetId();            

            var bikesQuery = bikeService.AllBikes();

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

            var totalBikes = bikesQuery.Count();

            var bikes = bikesQuery
                .Skip((query.CurrentPage - 1) * AllBikesViewModel.BikesPerPage)
                .Take(AllBikesViewModel.BikesPerPage)
                .AsQueryable()
                .ProjectTo<BikeListingViewModel>(this.selectionMapper);           

            var bikeBrands = bikeService.Brands();

            query.TotalBikesCount = totalBikes;
            query.Brands = bikeBrands;
            query.Bikes = bikes;

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.sellerService.IsValidSeller(this.User.GetId()))
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            return View(new AddBikeFormModel
            {
                Categories = bikeService.GetAllCategories()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddBikeFormModel bike)
        {
            var sellerId = bikeService.GetSellerId(this.User.GetId());

            if (sellerId == 0)
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            if (!bikeService.CheckCategoryId(bike.CategoryId))
            {
                this.ModelState.AddModelError(nameof(bike.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                bike.Categories = bikeService.GetAllCategories();
                return View(bike);
            }

            var bikeServiceModel = 
                mapper.Map<AddBikeFormModel, AddBikeServiceModel>(bike);

            bikeServiceModel.SellerId = sellerId;

            bikeService.Add(bikeServiceModel);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var bike = this.bikeService.Details(id);
            return View(bike);
        }

        [Authorize]
        public IActionResult Rent(int id)
        {
            if (!this.buyerService.IsBuyer(this.User.GetId()))
            {
                return RedirectToAction(nameof(BuyersController.Become), "Buyers");
            }

            var bike = this.bikeService.Details(id);
            return View(bike);
        }


        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.sellerService.IsValidSeller(userId))
            {
                return RedirectToAction(nameof(SellersController.Become), "Dealers");
            }

            var bike = this.bikeService.Details(id);

            if(bikeService.GetUserId(id) != userId)
            {
                return Unauthorized();
            }

            var bikeData = this.mapper.Map<DetailsServiceModel, AddBikeServiceModel>(bike);

            bikeData.Categories = this.bikeService.GetAllCategories();

            return View(bikeData);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, AddBikeFormModel bike)
        {
            var sellerId = this.sellerService.IdUser(this.User.GetId());

            if(sellerId == 0)
            {
                return RedirectToAction(nameof(SellersController.Become), "Dealers");
            }

            if (!this.bikeService.CheckCategoryId(bike.CategoryId))
            {
                this.ModelState.AddModelError(nameof(bike.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                bike.Categories = this.bikeService.GetAllCategories();

                return View(bike);
            }

            if (!this.bikeService.IsSeller(id, sellerId))
            {
                return BadRequest();
            }

            var bikeEdited = this.bikeService
                .Edit(id,
                      bike.Brand,
                      bike.Model,
                      bike.Description,
                      bike.ImageUrl,
                      bike.Year,
                      bike.CategoryId);

            if (!bikeEdited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        public IActionResult Delete(int id)
        {
            var userId = this.User.GetId();

            if (!this.sellerService.IsValidSeller(userId))
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            if(this.bikeService.GetUserId(id) != userId)
            {
                return Unauthorized();
            }

            this.bikeService.Delete(id);

            return RedirectToAction(nameof(BikeController.All), "Bike");
        }
    }

}
