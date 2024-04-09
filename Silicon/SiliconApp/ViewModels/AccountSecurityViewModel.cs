using SiliconApp.Entities;
using SiliconApp.Models;

namespace SiliconApp.ViewModels
{
    public class AccountSecurityViewModel
    {
        public UserEntity? UserEntity { get; set; }

        public AccountSecurityPasswordModel PasswordForm { get; set; } = new AccountSecurityPasswordModel();

        public AccountSecurityDeleteAccountModel DeleteAccountForm { get; set; } = new AccountSecurityDeleteAccountModel();
    }
}
