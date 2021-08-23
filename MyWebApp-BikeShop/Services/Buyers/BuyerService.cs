namespace MyWebApp_BikeShop.Services.Buyers
{
    using AutoMapper;
    using MyWebApp_BikeShop.Data;
    using MyWebApp_BikeShop.Data.Models;
    using MyWebApp_BikeShop.Services.Buyers.Model;
    using System.Linq;

    public class BuyerService : IBuyerService
    {
        private BikeShopDbContext data;
        private IMapper mapper;
        public BuyerService(BikeShopDbContext data, IMapper mapper)
        {
            this.mapper = mapper;
            this.data = data;
        }

        public void Become(BecomeABuyerServiceModel model)
        {
            var buyer = mapper.Map<BecomeABuyerServiceModel, Buyer>(model);

            this.data.Buyers.Add(buyer);
            this.data.SaveChanges();
        }

        public bool IsBuyer(string userId)
           => this.data.Buyers.Any(l => l.UserId == userId);
    }
}
