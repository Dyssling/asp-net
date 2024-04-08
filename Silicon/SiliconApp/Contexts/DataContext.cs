using Microsoft.EntityFrameworkCore;

namespace SiliconApp.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
