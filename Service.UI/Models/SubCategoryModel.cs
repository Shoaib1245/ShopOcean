using System.ComponentModel.DataAnnotations.Schema;

namespace Service.UI.Models
{
    public class SubCategoryModel
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string? SubCategoryName { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }
        public string? SubCategoryImage { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public int? SellerId { get; set; }
    }
}
