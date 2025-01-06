using System.ComponentModel.DataAnnotations;

namespace Service.Seller.APIs.Features.Size.Core
{
    public class SizeModel
    {
        [Key]
        public int Id { get; set; }
        public string? SizeName { get; set; }
        public int? IsDeleted { get; set; }
        public int? Status { get; set; }
    }
}
