using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<IEnumerable<CourseEntity>> GetAllFilteredAsync(string categoryId, string search)
        {
            try
            {
                var query = _context.Courses.Include(x => x.Category).AsQueryable(); //Här gör vi en IQueryable, som alltså sedan kommer bli en SQL query

                if (int.TryParse(categoryId, out var intCategoryId)) //categoryId parseas till ett int, och om den lyckas så körs denna satsen
                {
                    if (intCategoryId != 0) //Filtreringen nedan sker inte om kategori valet är 0 (alla kategorier)
                    {
                        query = query.Where(x => x.CategoryId == intCategoryId); //Alla kurser vars CategoryId är samma som den angivna parametern hämtas
                    }
                }

                if (!search.IsNullOrEmpty())
                {
                    query = query.Where(x => x.Title.Contains(search) || x.Author.Contains(search)); //Här filtrerar vi vidare från föregående filtreringen om TryParse lyckades, annars används bara detta filtret. Här går vi på söktermen, och inkluderar även alla resultat där söktermen hittas i Author
                }

                var list = await query.ToListAsync(); //Informationen omvandlas till en vanlig lista

                return list;
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
