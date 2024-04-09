using SiliconApp.Entities;
using SiliconApp.Models;
using System.ComponentModel.DataAnnotations;

namespace SiliconApp.ViewModels
{
    public class AccountDetailsViewModel
    {
        public UserEntity? UserEntity { get; set; } //Denna kommer ju inte vara null, men om jag sätter den som = null! så hamnar den i ModelState och blir validerad...
        public AccountDetailsBasicInfoModel BasicInfoForm { get; set; } = new AccountDetailsBasicInfoModel();
        public AccountDetailsAddressModel AddressForm { get; set; } = new AccountDetailsAddressModel();
    }
}
