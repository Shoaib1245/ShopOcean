using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.FrontEnd.APIs.Features.Review_Comments.Core
{
    public class ReviewAndCommentsModel
    {
        [Key]
        public int Id { get; set; }
        public string? Image { get; set; }
        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }
        public string? Rating { get; set; }
        public string? Comments { get; set; }
        [NotMapped]
        public string? CustomerName { get; set; }
        public int? status { get; set; }
        public DateTime? Created { get; set; }
    }
}
