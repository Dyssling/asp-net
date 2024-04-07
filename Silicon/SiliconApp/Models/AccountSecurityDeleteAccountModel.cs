using SiliconApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SiliconApp.Models
{
    public class AccountSecurityDeleteAccountModel
    {
        [Display(Name = "Yes, I want to delete my account.")]
        [CheckBoxRequired(ErrorMessage = "You must confirm that you want to delete your account.")]
        [Required(ErrorMessage = "You must confirm that you want to delete your account.")]

        public bool ConfirmDelete { get; set; } = false;

        public string? DeleteAccountFormValue { get; set; }
    }
}
