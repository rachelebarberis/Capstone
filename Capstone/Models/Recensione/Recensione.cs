

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Capstone.Models
    {
        public class Recensione
        {

        [Key]
        public int IdRecensione { get; set; }
        public DateOnly CreatedAt { get; set; } 

        [Required]
        public string UserId { get; set; }

      
        [ForeignKey("UserId")]
     
        public ApplicationUser User { get; set; }


        [Required(ErrorMessage = "Il contenuto della recensione è obbligatorio.")]
        [StringLength(500, ErrorMessage = "La recensione non può superare i 500 caratteri.")]
        public string Commento { get; set; }

        [Range(1, 5, ErrorMessage = "La valutazione deve essere compresa tra 1 e 5.")]
        public int Valutazione { get; set; }

        [ForeignKey("IdItinerario")]
        public int IdItinerario { get; set; }

     
        public Itinerario Itinerario { get; set; }

       
    }
    }

