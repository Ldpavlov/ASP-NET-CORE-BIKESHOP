namespace MyWebApp_BikeShop.Services.Sellers
{
    using MyWebApp_BikeShop.Services.Bikes.Models;

    public interface ISellersService
    {
        public bool IsValidSeller(string userId);
        public void Become(BecomeSellerServiceModel seller);
        public int IdUser(string userId);
    }
}
