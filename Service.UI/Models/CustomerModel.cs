namespace Service.UI.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Contact { get; set; }
        public string? Address { get; set; }
        public string? ZipCode { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public string? conformpassword { get; set; }
        public int? Status { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
