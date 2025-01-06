using System.ComponentModel.DataAnnotations;

namespace Service.UI.Models
{
    public class OrderPlacedModel
    {
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
            public string? Contact { get; set; }
            public int? Status { get; set; }
            public string? CustomerName { get; set; }
            public string? Image { get; set; }
        


    }
}
