using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Capstone.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }


        public ICollection<ApplicationUserRole>? UserRoles { get; set; }
       
    }
}
