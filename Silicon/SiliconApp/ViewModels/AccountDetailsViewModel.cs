using SiliconApp.Entities;
using SiliconApp.Models;
using System.ComponentModel.DataAnnotations;

namespace SiliconApp.ViewModels
{
    public class AccountDetailsViewModel
    {
        public UserEntity UserEntity { get; set; } = null!;
        public AccountDetailsBasicInfoModel BasicInfoForm { get; set; } = new AccountDetailsBasicInfoModel();
        public AccountDetailsAddressModel AddressForm { get; set; } = new AccountDetailsAddressModel();
    }
}
