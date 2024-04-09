using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore;
using SiliconApp.Contexts;
using SiliconApp.Entities;
using System.Diagnostics;
using System.Linq.Expressions;

namespace SiliconApp.Repositories
{
    public class UserRepository : BaseRepository<UserEntity, DataContext>
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        //I nedanstående overrides  gör jag en Include för att göra en JOIN sats på User entiteterna man får tillbaka, så att man även får tillbaka dess addressinformation.

        public override async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            try
            {
                var entityList = await _context.Users.Include(x => x.Address).ToListAsync();
                return entityList;

            }
            catch { }

            return null!;
        }

        public override async Task<UserEntity> GetOneAsync(Expression<Func<UserEntity, bool>> expression)
        {
            try
            {
                var entity = await _context.Users.Include(x => x.Address).FirstOrDefaultAsync(expression);
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
