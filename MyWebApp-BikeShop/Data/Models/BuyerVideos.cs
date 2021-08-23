namespace MyWebApp_BikeShop.Data.Models
{  
    public class BuyerVideos
    {
        public int BuyerId { get; set; }
        public Buyer Student { get; set; }

        public int VideoId { get; set; }
        public Video Video { get; set; }
    }
}
