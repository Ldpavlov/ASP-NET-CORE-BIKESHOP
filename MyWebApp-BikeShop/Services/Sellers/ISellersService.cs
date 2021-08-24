namespace MyWebApp_BikeShop.Services.Sellers
{
    public interface ISellersService
    {
        public bool IsValidSeller(string userId);
        public void Become(BecomeSellerServiceModel seller);
        public int IdUser(string userId);
    }
}
