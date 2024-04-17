namespace SiliconAPI.Models
{
    public class ContactModel
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Service { get; set; }

        public string Message { get; set; } = null!;
    }
}
