using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Service.FrontEnd.APIs.Features.WishList.Core
{
    public class ExtraWistlistdataModel
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? CustomerId { get; set; }
        public string? Price { get; set; }
        public string? Discount { get; set; }
        public string? ProductName { get; set; }
        public string? Image { get; set; }
    }
}
