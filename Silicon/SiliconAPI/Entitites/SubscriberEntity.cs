﻿namespace SiliconAPI.Entitites
{
    public class SubscriberEntity
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public bool DailyNewsletter { get; set; } = false;
        public bool AdvertisingUpdates { get; set; } = false;
        public bool WeekInReview { get; set; } = false;
        public bool EventUpdates { get; set; } = false;
        public bool StartupsWeekly { get; set; } = false;
        public bool Podcasts { get; set; } = false;
    }
}
