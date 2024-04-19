using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SiliconApp.Entities
{
    public class UserEntity : IdentityUser
    {
        [ProtectedPersonalData]
        public string FirstName { get; set; } = null!;

        [ProtectedPersonalData]
        public string LastName { get; set; } = null!;

        public string? Bio { get; set; }

        public int? AddressId { get; set; }
        public AddressEntity? Address { get; set; }

        public bool IsExternal { get; set; } = false;

        public string? CourseList { get; set; }
    }
}
