namespace MyWebApp_BikeShop.Models.Bikes
{
    public class BikeListingViewModel
    {
        public string Brand { get; init; }

        public string Model { get; init; }

        public string ImageUrl { get; init; }

        public int Id { get; init; }

        public string Description { get; init; }

        public int Year { get; init; }

        public string Category { get; init; }

        public string UserId { get; set; }

    }
}
