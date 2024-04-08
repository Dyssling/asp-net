using System.ComponentModel.DataAnnotations;

namespace SiliconApp.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string SecurityKey { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Bio { get; set; }

        public int? AddressId { get; set; }
        public AddressEntity? Address { get; set; }
    }
}
