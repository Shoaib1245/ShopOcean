namespace Service.UI.Models
{
    public class ChatSystemModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserType { get; set; }
        public string? Message { get; set; }
        public DateTime? MessageTime { get; set; }
        public bool? IsDeleted { get; set; }
    }
}


