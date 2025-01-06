using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.FrontEnd.APIs.Features.Cart.Core
{
    public class CartModel
    {
        [Key]
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        [NotMapped]
        public string? ProductName { get; set; }
        [NotMapped]
        public string? Image { get; set; }
        public int? SizeId { get; set; }
        public decimal? Price { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? Quantity { get; set; }
    }
}
