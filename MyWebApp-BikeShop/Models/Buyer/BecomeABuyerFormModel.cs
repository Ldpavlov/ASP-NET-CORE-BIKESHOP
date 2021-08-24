namespace MyWebApp_BikeShop.Models.Buyer
{
    using System.ComponentModel.DataAnnotations;
    using static MyWebApp_BikeShop.Data.DataConstants;

    public class BecomeABuyerFormModel
    {
        [Required]
        [StringLength(BuyerNameMaxLength, MinimumLength = BuyerNameMinLength)]
        public string FullName { get; set; }

        [Required]
        [StringLength(BuyerPhoneMaxLength, MinimumLength = BuyerPhoneMinLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; }

    }
}
