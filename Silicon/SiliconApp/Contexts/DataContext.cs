using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiliconAPI.Entitites;
using SiliconApp.Entities;

namespace SiliconApp.Contexts
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<SubscriberEntity> Subscribers { get; set; }
    }
}
