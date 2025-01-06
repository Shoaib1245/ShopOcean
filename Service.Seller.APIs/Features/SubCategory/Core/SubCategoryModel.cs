using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Seller.APIs.Features.SubCategory.Core
{
    public class SubCategoryModel
    {
        [Key]
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string? SubCategoryName { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }
        public string? SubCategoryImage { get; set; }
        //[NotMapped]
        //public IFormFile? ImageUrl { get; set; }
        public int? SellerId { get; set; }
    }
}
