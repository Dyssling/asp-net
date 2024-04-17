using Microsoft.EntityFrameworkCore;
using SiliconAPI.Entities;
using SiliconAPI.Entitites;

namespace SiliconAPI.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<SubscriberEntity> Subscribers { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }
    }
}
