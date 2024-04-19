using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SiliconApp.Models
{
    public class ContactModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Full name", Prompt = "Enter your full name")]
        [Required(ErrorMessage = "You must enter your full name.")]
        [MinLength(2, ErrorMessage = "You must enter a valid full name.")]

        public string FullName { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address", Prompt = "Enter your email address")]
        [Required(ErrorMessage = "You must enter your email address.")]
        [RegularExpression("^\\w+([.-]?\\w+)*@\\w+([.-]?\\w+)*(\\.\\w{2,})+$", ErrorMessage = "You must enter a valid email address.")]

        public string Email { get; set; } = null!;

        public string? Service { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Message", Prompt = "Enter your message here...")]
        [Required(ErrorMessage = "You must enter a message.")]

        public string Message { get; set; } = null!;
    }
}
