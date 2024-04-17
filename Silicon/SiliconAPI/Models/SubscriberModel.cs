using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiliconAPI.Models
{
    public class SubscriberModel
    {
        [Required]
        [RegularExpression("^\\w+([.-]?\\w+)*@\\w+([.-]?\\w+)*(\\.\\w{2,})+$", ErrorMessage = "You must enter a valid email address.")]
        [DefaultValue("string")] //Av någon anledning blir default value en galen sträng om jag inte ställer in den själv, p.g.a regex
        public string Email { get; set; } = null!;
    }
}
