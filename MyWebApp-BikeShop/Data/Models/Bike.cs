namespace MyWebApp_BikeShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Bike
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(BikeBrandMaxLength)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(BikeModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(BikeDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Range(BikeYearMinValue, BikeYearMaxValue)]
        public int Year { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }

        public int SellerId { get; init; }

        public Seller Seller { get; set; }

        [Required]
        public string PurchaseUrl { get; set; }
    }
}
