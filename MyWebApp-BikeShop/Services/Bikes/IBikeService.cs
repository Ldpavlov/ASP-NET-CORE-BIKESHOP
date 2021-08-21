using System.Collections;
using System.Collections.Generic;

namespace MyWebApp_BikeShop.Services.Bikes
{
    public interface IBikeService
    {
        BikeServiceModel All(
            string brand,
            string searchTerm,
            int currentPage,
            int bikesPerPage);

        IEnumerable<string> AllBikeBrands();
    }
}
