using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Seller.APIs.Features.Category.Core
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        public int? SellerId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Status { get; set; }
        public string? Image { get; set; }
      
    }
}
