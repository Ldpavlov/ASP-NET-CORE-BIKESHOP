namespace MyWebApp_BikeShop.Services.Bikes
{
    using MyWebApp_BikeShop.Data;
    using MyWebApp_BikeShop.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class BikeService : IBikeService
    {
        private readonly BikeShopDbContext data;

        public BikeService(BikeShopDbContext data)
            => this.data = data;

        public BikeServiceModel All(string brand, string searchTerm, int currentPage, int bikesPerPage)
        {
            var bikesQuery = this.data.Bikes.AsQueryable();

            if (!string.IsNullOrEmpty(brand))
            {
                bikesQuery = bikesQuery.Where(b => b.Brand == brand);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bikesQuery = bikesQuery.Where(b =>
                (b.Brand + " " + b.Model).ToLower().Contains(searchTerm.ToLower()) ||
                b.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            var totalBikes = bikesQuery.Count();

            var bikes = bikesQuery
                .Skip((currentPage - 1) * bikesPerPage)
                .Take(bikesPerPage)
                .Select(b => new BikeServiceModel
                {
                    Id = b.Id,
                    Brand = b.Brand,
                    Model = b.Model,
                    Year = b.Year,
                    ImageUrl = b.ImageUrl,
                    Category = b.Category.Name
                })
                .ToList();

            return new BikeServiceModel
            {
                TotalBikesCount = totalBikes,
                CurrentPage = currentPage,
                BikesPerPage = bikesPerPage
            };
        }

        public IEnumerable<string> AllBikeBrands()
        => this.data
                .Bikes
                .Select(b => b.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();
    }
}
