using SiliconApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SiliconApp.Models
{
    public class HomeNewsletterModel
    {
        [Display(Name = " Daily Newsletter")]
        public bool DailyNewsletter { get; set; } = false;

        [Display(Name = " Advertising Updates")]
        public bool AdvertisingUpdates { get; set; } = false;

        [Display(Name = " Week in Review")]
        public bool WeekInReview { get; set; } = false;

        [Display(Name = " Event Updates")]
        public bool EventUpdates { get; set; } = false;

        [Display(Name = " Startups Weekly")]
        public bool StartupsWeekly { get; set; } = false;

        [Display(Name = " Podcasts")]
        public bool Podcasts { get; set; } = false;

        [DataType(DataType.EmailAddress)]
        [Display(Prompt = "Your Email")]
        [Required(ErrorMessage = "You must enter your email address.")]
        [RegularExpression("^\\w+([.-]?\\w+)*@\\w+([.-]?\\w+)*(\\.\\w{2,})+$", ErrorMessage = "You must enter a valid email address.")]

        public string Email { get; set; } = null!;
    }
}
