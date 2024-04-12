using System.ComponentModel.DataAnnotations;

namespace SiliconApp.Models
{
    public class AccountDetailsBasicInfoModel
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

        [DataType(DataType.Text)]
        [Display(Name = "Phone", Prompt = "Enter your phone")]
        [RegularExpression("^[0-9*#+ .()-]{7,}$", ErrorMessage = "The entered phone number is invalid.")]

        public string? Phone { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Bio", Prompt = "Add a short bio...")]

        public string? Bio { get; set; }

        public string? BasicInfoFormValue { get; set; }
    }
}
