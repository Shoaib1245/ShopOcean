namespace Service.UI.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Image { get; set; }
        public int? SizeId { get; set; }
        public decimal? Price { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? Quantity { get; set; }
    }
}
