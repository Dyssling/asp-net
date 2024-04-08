using System.ComponentModel.DataAnnotations;

namespace SiliconApp.Entities
{
    public class AddressEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }

        public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}
