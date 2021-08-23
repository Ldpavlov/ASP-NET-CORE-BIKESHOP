namespace MyWebApp_BikeShop.Services.Sellers
{
    using System.ComponentModel.DataAnnotations;

    public class BecomeSellerServiceModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string UserId { get; set; }
    }
}
