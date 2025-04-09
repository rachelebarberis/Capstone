

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models
    {
        public class Recensione
        {
            [Key]
            public int IdRecensione { get; set; }

            [Required(ErrorMessage = "L'ID utente è obbligatorio.")]
            public string UserId { get; set; }  

            [Required(ErrorMessage = "Il contenuto della recensione è obbligatorio.")]
            [StringLength(500, ErrorMessage = "La recensione non può superare i 500 caratteri.")]
            public string Testo { get; set; } 

            [Range(1, 5, ErrorMessage = "La valutazione deve essere compresa tra 1 e 5.")]
            public int Valutazione { get; set; }  

            [ForeignKey("Itinerario")]
            public int IdItinerario { get; set; }  
            public Itinerario Itinerario { get; set; }  

            [NotMapped]
            public string NomeUtente { get; set; } 
        }
    }

