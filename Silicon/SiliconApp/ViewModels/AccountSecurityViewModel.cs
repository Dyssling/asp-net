using SiliconApp.Models;

namespace SiliconApp.ViewModels
{
    public class AccountSecurityViewModel
    {
        public AccountSecurityPasswordModel PasswordForm { get; set; } = new AccountSecurityPasswordModel();

        public AccountSecurityDeleteAccountModel DeleteAccountForm { get; set; } = new AccountSecurityDeleteAccountModel();
    }
}
