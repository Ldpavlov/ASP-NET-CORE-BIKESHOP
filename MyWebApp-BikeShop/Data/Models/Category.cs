namespace MyWebApp_BikeShop.Data.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public IEnumerable<Bike> Bikes { get; init; } = new List<Bike>();
    }
}
