using SiliconAPI.Contexts;
using SiliconAPI.Entitites;

namespace SiliconAPI.Repositories
{
    public class ContactRepository : BaseRepository<ContactEntity, DataContext>
    {
        private readonly DataContext _context;
        public ContactRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
