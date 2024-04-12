using SiliconApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SiliconApp.Models
{
    public class SignInModel
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address", Prompt = "Enter your email address")]
        [Required(ErrorMessage = "You must enter your email address.")]
        [RegularExpression("^\\w+([.-]?\\w+)*@\\w+([.-]?\\w+)*(\\.\\w{2,})+$", ErrorMessage = "You must enter a valid email address.")]

        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "********")]
        [Required(ErrorMessage = "You must enter a password.")]

        public string Password { get; set; } = null!;

        [Display(Name = "Remember me")]

        public bool RememberMe { get; set; } = false;
    }
}
