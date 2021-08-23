namespace MyWebApp_BikeShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Buyer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(BuyerNameMaxLength, MinimumLength = BuyerNameMinLength)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(BuyerPhoneMaxLength, MinimumLength = BuyerPhoneMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
