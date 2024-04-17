using SiliconAPI.Contexts;
using SiliconAPI.Entitites;

namespace SiliconAPI.Repositories
{
    public class SubscriberRepository : BaseRepository<SubscriberEntity, DataContext>
    {
        private readonly DataContext _context;
        public SubscriberRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
