namespace MyWebApp_BikeShop.Services.Bikes.Models
{
    using System.Collections.Generic;

    public class AddBikeServiceModel
    {        
        public string Brand { get; init; }  
        public string Model { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
        public int Year { get; init; }
        public int CategoryId { get; init; }
        public int SellerId { get; set; }

        public IEnumerable<BikeCategoryServiceModel> Categories { get; set; }
    }
}
