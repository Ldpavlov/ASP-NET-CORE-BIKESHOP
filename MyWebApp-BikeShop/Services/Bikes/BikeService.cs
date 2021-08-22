namespace MyWebApp_BikeShop.Services.Bikes
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using MyWebApp_BikeShop.Data;
    using MyWebApp_BikeShop.Data.Models;
    using MyWebApp_BikeShop.Models.Bikes;
    using MyWebApp_BikeShop.Services.Bikes.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class BikeService : IBikeService
    {
        private readonly BikeShopDbContext data;
        private readonly IConfigurationProvider mapper;

        public BikeService(BikeShopDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IEnumerable<AllBikeServiceModel> AllBikes()
            => data.Bikes
                .OrderByDescending(c => c.Id)
                .Include(c => c.Category)
                .Include(c => c.Seller)
                .Select(c => new AllBikeServiceModel()
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl,
                    Year = c.Year,
                    Description = c.Description,
                    Category = c.Category.Name,
                    UserId = c.Seller.UserId
                })
            .ToList();

        private string GetCategoryName(int id)
            => data.Categories.FirstOrDefault(c => c.Id == id).Name;

        public int GetSellerId(string userId)
            =>  this.data
                .Sellers
                .Where(s => s.UserId == userId)
                .Select(s => s.Id)
                .FirstOrDefault();

        public void Add(AddBikeServiceModel bike)
        {
            var bikeData = new Bike
            {
                Brand = bike.Brand,
                Model = bike.Model,
                Description = bike.Description,
                ImageUrl = bike.ImageUrl,
                CategoryId = bike.CategoryId,
                Year = bike.Year,
                SellerId = bike.SellerId
            };

            this.data.Bikes.Add(bikeData);
            this.data.SaveChanges();
        }

        public bool CheckCategoryId(int id)
            => this.data.Categories.Any(c => c.Id == id);

        public int TotalBikes()
            => this.data.Bikes.Count();

        public IEnumerable<string> Brands()
            => this.data
                .Bikes
                .Select(b => b.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

        public IEnumerable<BikeCategoryServiceModel> GetAllCategories() 
            => this.data
                .Categories
                .Select(c => new BikeCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
            .ToList();


        public DetailsServiceModel Details(int id)
            => this.data
                .Bikes
                .Where(b => b.Id == id)
                .ProjectTo<DetailsServiceModel>(this.mapper)
                .FirstOrDefault();
       
        public bool Edit(int id,
            string brand, 
            string model, 
            string description, 
            string imageUrl, 
            int year, 
            int categoryId)
        {
            var bike = this.data.Bikes.Find(id);

            if(bike == null)
            {
                return false;
            }

            bike.Brand = brand;
            bike.CategoryId = categoryId;
            bike.Model = model;
            bike.Description = description;
            bike.ImageUrl = imageUrl;
            bike.Year = year;

            this.data.SaveChanges();

            return true;
        }

        public string GetUserId(int id) 
            =>  this.data
               .Bikes
               .Include(b => b.Seller)
               .Where(b => b.Id == id)
               .Select(b => b.Seller.UserId)
               .FirstOrDefault();

        public bool IsSeller(int bikeId, int sellerId)
            => this.data
                .Bikes
                .Any(c => c.Id == bikeId && c.SellerId == sellerId);


    }
}
