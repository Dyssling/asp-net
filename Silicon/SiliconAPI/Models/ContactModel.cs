using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiliconAPI.Models
{
    public class ContactModel
    {
        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        [RegularExpression("^\\w+([.-]?\\w+)*@\\w+([.-]?\\w+)*(\\.\\w{2,})+$", ErrorMessage = "You must enter a valid email address.")]
        [DefaultValue("string")]
        public string Email { get; set; } = null!;

        public string? Service { get; set; }

        [Required]
        public string Message { get; set; } = null!;
    }
}
