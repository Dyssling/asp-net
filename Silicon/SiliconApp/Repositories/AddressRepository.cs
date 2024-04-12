using SiliconApp.Contexts;
using SiliconApp.Entities;

namespace SiliconApp.Repositories
{
    public class AddressRepository : BaseRepository<AddressEntity, DataContext>
    {
        public AddressRepository(DataContext context) : base(context)
        {
        }
    }
}
