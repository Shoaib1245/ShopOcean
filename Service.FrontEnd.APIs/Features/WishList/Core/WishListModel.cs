using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.FrontEnd.APIs.Features.WishList.Core
{
    public class WishListModel
    {
        [Key]
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? CustomerId { get; set; }
        public string? Price { get; set; }
        public string? Discount { get; set; }
      
    }
}
