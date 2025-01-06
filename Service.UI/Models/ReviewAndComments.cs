namespace Service.UI.Models
{
    public class ReviewAndComments
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public int? ProductId { get; set; }
        public string? Rating { get; set; }
        public string? Comments { get; set; }
        public int? status { get; set; }
        public DateTime? Created { get; set; }
    }
}
