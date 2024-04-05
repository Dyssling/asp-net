using SiliconApp.Models;
using System.ComponentModel.DataAnnotations;

namespace SiliconApp.ViewModels
{
    public class AccountDetailsViewModel
    {
        public AccountDetailsBasicInfoModel BasicInfoForm { get; set; } = new AccountDetailsBasicInfoModel();
        public AccountDetailsAddressModel AddressForm { get; set; } = new AccountDetailsAddressModel();
    }
}
