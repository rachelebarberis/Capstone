using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;



namespace Capstone.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [NotMapped]
        public IFormFile? ImgUser { get; set; }  // usato solo per l'upload da form

        public string? ImgUserPath { get; set; } // percorso fisico (es: "images/users/nome.jpg")

        public ICollection<Recensione> Recensioni { get; set; }


        public Carrello Carrello { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }

    }
}
