namespace MyWebApp_BikeShop.Models.Sellers
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class BecomeSellerFormModel
    {
        [Required]
        [StringLength(SellerNameMaxLength, MinimumLength = SellerNameMinLength)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(SellerPhoneMaxLength, MinimumLength = SellerPhoneMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
