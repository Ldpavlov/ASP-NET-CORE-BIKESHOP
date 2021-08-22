namespace MyWebApp_BikeShop.Services.Bikes
{
    using MyWebApp_BikeShop.Services.Bikes.Models;
    using System.Collections.Generic;

    public interface IBikeService
    {
        public IEnumerable<AllBikeServiceModel> AllBikes();
        public int GetSellerId(string userId);
        public void Add(AddBikeServiceModel bike);
        public bool CheckCategoryId(int id);
        public int TotalBikes();
        public IEnumerable<string> Brands();
        public IEnumerable<BikeCategoryServiceModel> GetAllCategories();
        public DetailsServiceModel Details(int id);
    }
}
