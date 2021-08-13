namespace MyWebApp_BikeShop.Models.Bikes
{
    using System.ComponentModel.DataAnnotations;

    public class DetailsBikeViewModel
    { 
        public string Brand { get; init; }

        public string Model { get; init; }
        
        public string Description { get; init; }

        [Display(Name = "Image URL")]        
        public string ImageUrl { get; init; }

        public int Year { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }
    }
}
