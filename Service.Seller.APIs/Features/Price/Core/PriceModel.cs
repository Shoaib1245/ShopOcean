using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Seller.APIs.Features.Price.Core
{
    public class PriceModel
    {
        [Key]
        public int Id { get; set; }
        public int? SizeId { get; set; }
        [NotMapped]
        public string? SizeName { get; set; }
        public decimal? Price { get; set; }
        public int? ProductId { get; set; }
        public int? IsDeleted { get; set; }
    }
}
