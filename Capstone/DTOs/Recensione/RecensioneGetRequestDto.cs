using System.ComponentModel.DataAnnotations;
using Capstone.Models;

namespace Capstone.DTOs.Recensione
{
    public class RecensioneGetRequestDto
    {
        [Key]
        public int IdRecensione { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Creazione")]
        public DateOnly CreatedAt { get; set; }

        [Required(ErrorMessage = "Il commento è obbligatorio")]
        [StringLength(1000, ErrorMessage = "Il commento non può superare i 1000 caratteri")]
        public string Commento { get; set; }

        [Range(1, 5, ErrorMessage = "La valutazione deve essere tra 1 e 5")]
        public int Valutazione { get; set; }


        [Required]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Nome Utente")]
        public string NomeUtente { get; set; }

        [Required]
        [Display(Name = "Email Utente")]
        public string Email { get; set; }

        [Display(Name = "Immagine Profilo")]
        public string? ImgUserPath { get; set; }

        [Required]
        public int IdItinerario { get; set; }

        [Required]
        [Display(Name = "Titolo Itinerario")]
        public string TitoloItinerario { get; set; }
    }
}
