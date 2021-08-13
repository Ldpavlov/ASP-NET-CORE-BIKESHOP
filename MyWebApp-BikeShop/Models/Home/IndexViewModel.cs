namespace MyWebApp_BikeShop.Models.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalBikes { get; init; }

        public int TotalUsers { get; init; }

        public int TotalRents { get; init; }

        public List<BikeIndexViewModel> Bikes { get; init; }
    }
}
