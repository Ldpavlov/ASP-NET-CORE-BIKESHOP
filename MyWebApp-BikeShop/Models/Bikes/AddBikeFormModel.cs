using MyWebApp_BikeShop.Data;

namespace MyWebApp_BikeShop.Models.Bikes
{
    using MyWebApp_BikeShop.Services.Bikes.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class AddBikeFormModel
    {
        [Required]
        [StringLength(BikeBrandMaxLength, MinimumLength = BikeBrandMinLength)]
        public string Brand { get; init; }

        [Required]
        [StringLength(BikeModelMaxLength,MinimumLength = BikeModelMinLength)]
        public string Model { get; init; }
        
        [Required]
        [StringLength(
            int.MaxValue, 
            MinimumLength = BikeDescriptionMinLength,
            ErrorMessage = "The field Description must be a string with minumum length of {1}.")]
        public string Description { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Range(BikeYearMinValue, BikeYearMaxValue)]
        public int Year { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<BikeCategoryServiceModel> Categories { get; set; }

    }
}
