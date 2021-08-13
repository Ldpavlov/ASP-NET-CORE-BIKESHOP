namespace MyWebApp_BikeShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; init; }

        public IEnumerable<Bike> Bikes { get; init; } = new List<Bike>();
    }
}
