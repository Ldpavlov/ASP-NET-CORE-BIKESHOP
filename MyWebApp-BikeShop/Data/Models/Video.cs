namespace MyWebApp_BikeShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Video
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public IEnumerable<BuyerVideos> Views { get; set; }
    }
}
