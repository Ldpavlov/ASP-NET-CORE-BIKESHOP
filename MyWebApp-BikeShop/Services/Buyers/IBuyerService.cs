namespace MyWebApp_BikeShop.Services.Buyers
{
    using MyWebApp_BikeShop.Services.Buyers.Model;

    public interface IBuyerService
    {
        public bool IsBuyer(string userId);
        public void Become(BecomeABuyerServiceModel model);
    }
}