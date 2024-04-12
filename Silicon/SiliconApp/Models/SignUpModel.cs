using System.ComponentModel.DataAnnotations;
using System.Runtime.ExceptionServices;
using SiliconApp.Helpers;

namespace SiliconApp.Models
{
    public class SignUpModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "First name", Prompt = "Enter your first name")]
        [Required(ErrorMessage = "You must enter your first name.")]
        [MinLength(2, ErrorMessage = "You must enter a valid first name.")]

        public string FirstName { get; set; } = null!;

        [DataType(DataType.Text)]
        [Display(Name = "Last name", Prompt = "Enter your last name")]
        [Required(ErrorMessage = "You must enter your last name.")]
        [MinLength(2, ErrorMessage = "You must enter a valid last name.")]

        public string LastName { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address", Prompt = "Enter your email address")]
        [Required(ErrorMessage = "You must enter your email address.")]
        [RegularExpression("^\\w+([.-]?\\w+)*@\\w+([.-]?\\w+)*(\\.\\w{2,})+$", ErrorMessage = "You must enter a valid email address.")]

        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "********")]
        [Required(ErrorMessage = "You must enter a password.")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$", ErrorMessage = "Password must be a minimum of 8 characters, and contain at least one number, one lowercase character, one uppercase character and one special character.")]

        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password", Prompt = "********")]
        [Required(ErrorMessage = "You must confirm your password.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]

        public string ConfirmPassword { get; set; } = null!;

        [Display(Name = "I agree to the Terms & Conditions.")]
        [CheckBoxRequired(ErrorMessage = "You must accept the Terms & Conditions.")]
        [Required(ErrorMessage = "You must accept the Terms & Conditions.")]

        public bool TermsAndConditions { get; set; } = false;
    }
}
