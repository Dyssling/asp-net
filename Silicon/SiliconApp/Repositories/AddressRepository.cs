using SiliconApp.Contexts;

namespace SiliconApp.Repositories
{
    public class AddressRepository : BaseRepository<AddressRepository, DataContext>
    {
        public AddressRepository(DataContext context) : base(context)
        {
        }
    }
}
