using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiliconApp.Models
{
    public class SubscriberModel
    { 
        public string Email { get; set; } = null!;

        public bool DailyNewsletter { get; set; } = false;
        public bool AdvertisingUpdates { get; set; } = false;
        public bool WeekInReview { get; set; } = false;
        public bool EventUpdates { get; set; } = false;
        public bool StartupsWeekly { get; set; } = false;
        public bool Podcasts { get; set; } = false;
    }
}
