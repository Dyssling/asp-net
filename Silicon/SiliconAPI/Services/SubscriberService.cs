using SiliconAPI.Entitites;
using SiliconAPI.Models;
using SiliconAPI.Repositories;

namespace SiliconAPI.Services
{
    public class SubscriberService
    {
        private readonly SubscriberRepository _repo;

        public SubscriberService(SubscriberRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> RegisterSubscriberAsync(SubscriberModel model)
        {
            try
            {
                var alreadyExists = await _repo.GetOneAsync(x => x.Email == model.Email); //Försöker hämta en entitet med samma email

                if (alreadyExists == null) //Om det inte finns en entitet med samma email
                {
                    var entity = new SubscriberEntity()
                    {
                        Email = model.Email,
                        DailyNewsletter = model.DailyNewsletter,
                        AdvertisingUpdates = model.AdvertisingUpdates,
                        WeekInReview = model.WeekInReview,
                        EventUpdates = model.EventUpdates,
                        StartupsWeekly = model.StartupsWeekly,
                        Podcasts = model.Podcasts
                    };

                    var result = await _repo.CreateAsync(entity);

                    if (result)
                    {
                        return "Success";
                    }
                }

                else
                {
                    return "Conflict"; //En entitet med samma email finns redan
                }

            }

            catch { }

            return "Error";
        }

        public async Task<bool> UnregisterSubscriberAsync(string email)
        {
            try
            {
                var result = await _repo.DeleteAsync(x => x.Email == email);

                if (result)
                {
                    return true;
                }
            }

            catch { }

            return false;
        }
    }
}
