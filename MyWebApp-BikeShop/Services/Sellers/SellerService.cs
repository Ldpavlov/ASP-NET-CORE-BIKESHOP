namespace MyWebApp_BikeShop.Services.Sellers
{
    using AutoMapper;
    using MyWebApp_BikeShop.Data;
    using MyWebApp_BikeShop.Data.Models;
    using System.Linq;

    public class SellerService : ISellersService
    {
        private readonly BikeShopDbContext data;
        private IMapper mapper;

        public SellerService(BikeShopDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }            

        public bool IsValidSeller(string userId)
            => this.data
                .Sellers
                .Any(s => s.UserId == userId);

        public void Become(BecomeSellerServiceModel seller)
        {
            var sellerData = mapper.Map<BecomeSellerServiceModel, Seller>(seller);         

            this.data.Sellers.Add(sellerData);
            this.data.SaveChanges();
        }

        public int IdUser(string userId)
            => this.data
                .Sellers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

    }
}
