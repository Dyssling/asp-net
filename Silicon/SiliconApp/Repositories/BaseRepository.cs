using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace SiliconApp.Repositories
{
    public abstract class BaseRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        public BaseRepository(TContext context)
        {
            _context = context;
        }

        public virtual async Task<bool> CreateAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();

                return true; //Om det gick att lägga till entiteten så får man ut true
            }
            catch (Exception ex) //Annars får man felmeddelandet i debuggern, och man får false tillbaka. På så vis kan man göra lite kontroller sedan i servicen
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression) //Denna är lite krånglig, men helt enkelt kommer man sätta in en expression här
        {
            try
            {
                var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression); //Expressionet kommer vara false i FirstOrDefault metoden när det inte stämmer överens med den "nuvarande" kolumnen/raden, annars är den true, och då får man ut rätt entitet
                if (entity != null) //Default värdet i detta fallet kommer vara null
                {
                    return entity;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null!; //Om entity förblir null (alltså om metoden går förbi if-satsen) så kommer man få tillbaka null i vilket fall som helst.
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                var entityList = await _context.Set<TEntity>().ToListAsync();
                return entityList; //ToList metoden returnerar aldrig null, därför behöver jag inte göra en check

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null!; //Metoden returnerar null om något skulle gå snett
            }
        }

        public virtual async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity entity)
        {
            try
            {
                var currentEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
                if (currentEntity != null)
                {
                    _context.Entry(currentEntity).CurrentValues.SetValues(entity);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;  //Metoden returnerar false om entiteten inte kunde hittas, eller om något går snett.
        }

        public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
                if (entity != null)
                {
                    _context.Set<TEntity>().Remove(entity);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }
    }
}

