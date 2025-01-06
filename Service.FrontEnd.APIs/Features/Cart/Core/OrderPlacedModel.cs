using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Service.FrontEnd.APIs.Features.Cart.Core
{
    public class OrderPlacedModel
    {
        [Key]
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Email { get; set; }
        public string? OrderNote { get; set; }
        public string? ZipCode { get; set; }
        public string? Contact{ get; set; }
        public int? Status { get; set; }

    }
}
