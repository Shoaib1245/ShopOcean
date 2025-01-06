using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Seller.APIs.Features.Product.Core
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int? UnitId { get; set; }
        public int? ColorId { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        [NotMapped]
        public string? Name { get; set; }
        [NotMapped]
        public string? SubCategoryName { get; set; }

        [NotMapped]
        public string? Prices { get; set; }
        [NotMapped]
        public string? Sizes { get; set; }
       
        [NotMapped]
        public int? SizeId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? Discount { get; set; }
        public int? Status { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsNewArrival { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; }
        public int? SellerId { get; set; }
    }
}
