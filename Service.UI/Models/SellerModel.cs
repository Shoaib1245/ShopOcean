using System.ComponentModel.DataAnnotations.Schema;

namespace Service.UI.Models
{
    public class SellerModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Cnic { get; set; }

        public string? Contact { get; set; }

        public string? DOB { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public string? NewPassword { get; set; }

        public string? ConformPassword { get; set; }

        public string? Image { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public int? Status { get; set; }

        public bool? IsDeleted { get; set; }
        public int? ZipCode { get; set; }
    }
}
