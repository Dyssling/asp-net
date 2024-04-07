using System.ComponentModel.DataAnnotations;

namespace SiliconApp.Models
{
    public class AccountSecurityPasswordModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Current password", Prompt = "********")]
        [Required(ErrorMessage = "You must enter your current password.")]

        public string CurrentPassword { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "New password", Prompt = "********")]
        [Required(ErrorMessage = "You must enter your new password.")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$", ErrorMessage = "Password must be a minimum of 8 characters, and contain at least one number, one lowercase character, one uppercase character and one special character.")]

        public string NewPassword { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password", Prompt = "********")]
        [Required(ErrorMessage = "You must confirm your new password.")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match.")]

        public string ConfirmNewPassword { get; set; } = null!;

        public string? PasswordFormValue { get; set; }
    }
}
