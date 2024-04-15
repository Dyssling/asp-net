using SiliconAPI.Contexts;
using SiliconAPI.Entities;

namespace SiliconAPI.Repositories
{
    public class CourseRepository : BaseRepository<CourseEntity, DataContext>
    {
        private readonly DataContext _context;
        public CourseRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
