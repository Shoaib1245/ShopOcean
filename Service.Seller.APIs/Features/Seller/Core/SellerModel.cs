using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Seller.APIs.Features.Seller.Core
{
    public class SellerModel
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Cnic { get; set; }

        public string? Contact { get; set; }

        public string? DOB { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        //[NotMapped]
        //public string? NewPassword { get; set; }
        //[NotMapped]
        //public string? ConformPassword { get; set; }

        public string? Image { get; set; }
        public int? Status { get; set; }

        public bool? IsDeleted { get; set; }
        public int? ZipCode { get; set; }
    }
}
