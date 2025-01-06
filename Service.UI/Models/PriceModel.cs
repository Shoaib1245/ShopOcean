namespace Service.UI.Models
{
    public class PriceModel
    {
        public int Id { get; set; }
        public int? SizeId { get; set; }
        public string? SizeName { get; set; }
        public decimal? Price { get; set; }
        public int? ProductId { get; set; }
        public int? IsDeleted { get; set; }
    }
}
