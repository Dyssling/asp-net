using Microsoft.EntityFrameworkCore;
using SiliconAPI.Entities;

namespace SiliconAPI.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CourseEntity> Courses { get; set; }
    }
}
