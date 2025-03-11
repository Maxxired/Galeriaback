using Microsoft.AspNetCore.Identity;

namespace Galeria.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsDeleted { get; set; }
        public string? AvatarURL { get; set; }
    }
}
