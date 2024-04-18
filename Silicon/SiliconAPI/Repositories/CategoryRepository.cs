using SiliconAPI.Contexts;
using SiliconAPI.Entitites;

namespace SiliconAPI.Repositories
{
    public class CategoryRepository : BaseRepository<CategoryEntity, DataContext>
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
