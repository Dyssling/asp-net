using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SiliconApp.Entities;

namespace SiliconApp.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }
    }
}
