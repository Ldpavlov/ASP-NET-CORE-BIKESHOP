namespace MyWebApp_BikeShop.Models
{
    using MyWebApp_BikeShop.Models.Bikes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllBikesViewModel
    {
        public const int BikesPerPage = 3;

        public string Brand { get; init; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public IEnumerable<BikeListingViewModel> Bikes { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public int CurrentPage { get; init; } = 1;

        public int TotalBikesCount { get; set; }
    }
}
