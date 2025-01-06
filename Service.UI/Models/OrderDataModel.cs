namespace Service.UI.Models
{
    public class OrderDataModel
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Email { get; set; }
        public string? OrderNote { get; set; }
        public string? ZipCode { get; set; }
        public string? Contact { get; set; }
        public int? Status { get; set; }
        public string? ProductName { get; set; }
        public string? Image { get; set; }
        public int? SizeId { get; set; }
        public decimal? Price { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? Quantity { get; set; }
        public int? OrderId { get; set; }
        public string? CustomerName { get; set; }

    }
}
