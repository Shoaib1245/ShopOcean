using System.ComponentModel.DataAnnotations.Schema;

namespace Service.UI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int? UnitId { get; set; }
        public int? ColorId { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public string? Name { get; set; }
        public string? Prices { get; set; }
        public string? Sizes { get; set; }
        public int? SizeId { get; set; }
        public string? SubCategoryName { get; set; }
        public int? Discount { get; set; }
        public int? Status { get; set; }
        public string? Image { get; set; }
        public List<IFormFile>? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsNewArrival { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; }
        public int? SellerId { get; set; }
    }
}
