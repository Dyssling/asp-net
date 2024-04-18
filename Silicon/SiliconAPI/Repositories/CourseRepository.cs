using Microsoft.EntityFrameworkCore;
using SiliconAPI.Contexts;
using SiliconAPI.Entities;
using System.Linq.Expressions;

namespace SiliconAPI.Repositories
{
    public class CourseRepository : BaseRepository<CourseEntity, DataContext>
    {
        private readonly DataContext _context;
        public CourseRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<CourseEntity>> GetAllAsync()
        {
            try
            {
                var entityList = await _context.Courses.Include(x => x.Category).ToListAsync();
                return entityList;

            }
            catch { }

            return null!;
        }

        public override async Task<CourseEntity> GetOneAsync(Expression<Func<CourseEntity, bool>> expression)
        {
            try
            {
                var entity = await _context.Courses.Include(x => x.Category).FirstOrDefaultAsync(expression);

                if (entity != null)
                {
                    return entity;
                }
            }
            catch { }

            return null!;
        }
    }
}
