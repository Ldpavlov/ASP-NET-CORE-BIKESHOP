using MyWebApp_BikeShop.Models.Bikes;
using System.Collections.Generic;

namespace MyWebApp_BikeShop.Services.Bikes
{
    public class BikeServiceModel
    {
        public int Id { get; init; }

        public string Brand { get; init; }

        public string Model { get; init; }

        public string ImageUrl { get; init; }

        public int Year { get; init; }

        public string Category { get; init; }

        public int BikesPerPage = 3;
        internal readonly IEnumerable<BikeListingViewModel> Bikes;

        public int CurrentPage { get; init; } = 9;

        public int TotalBikesCount { get; set; }
    }
}
