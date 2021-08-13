namespace MyWebApp_BikeShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Seller
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(SellerNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(SellerPhoneMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Bike> Bikes { get; init; } = new List<Bike>();
    }
}
