using System.ComponentModel.DataAnnotations;

namespace SiliconApp.Models
{
    public class AccountDetailsAddressModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Address line 1", Prompt = "Enter your address line")]
        [MinLength(2, ErrorMessage = "The entered address is invalid.")]

        public string? Address1 { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Address line 2", Prompt = "Enter your second address line")]

        public string? Address2 { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Postal code", Prompt = "Enter your postal code")]
        [MinLength(2, ErrorMessage = "The entered postal code is invalid.")]

        public string? PostalCode { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "City", Prompt = "Enter your city")]
        //Finns städer med namn som är kortare än 2 bokstäver, så gör ingen MinLength här.

        public string? City { get; set; }

        [DataType(DataType.Text)]
        public string? TestValue { get; set; }
    }
}
