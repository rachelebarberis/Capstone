using Microsoft.AspNetCore.Identity;

namespace Capstone.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
