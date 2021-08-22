namespace MyWebApp_BikeShop.Services.Sellers
{
    using MyWebApp_BikeShop.Data;
    using MyWebApp_BikeShop.Data.Models;
    using System.Linq;

    public class SellerService : ISellersService
    {
        private readonly BikeShopDbContext data;

        public SellerService(BikeShopDbContext data)
            => this.data = data;

        public bool IsValidSeller(string userId)
            => this.data
                .Sellers
                .Any(s => s.UserId == userId);

        public void Become(BecomeSellerServiceModel seller)
        {
            var sellerData = new Seller
            {
                Name = seller.Name,
                PhoneNumber = seller.PhoneNumber,
                UserId = seller.UserId
            };                

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
