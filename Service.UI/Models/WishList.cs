namespace Service.UI.Models
{
    public class WishList
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }
        public int? CustomerId { get; set; }
        public string? Price { get; set;}
        public string? Discount { get; set; }
        public string? ProductName { get; set; }
        public string? Image { get; set; }
    }
}
