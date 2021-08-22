namespace MyWebApp_BikeShop.Models.Home
{
    using MyWebApp_BikeShop.Models.Bikes;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalBikes { get; init; }

        //    public int TotalUsers { get; init; }

        //    public int TotalRents { get; init; }

        public List<BikeListingViewModel> Bikes { get; init; }
    }
}
