using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models
{
    public class ItinerarioGiorno
    {
        [Key]
        public int IdItinerarioGiorno { get; set; }

        [Required(ErrorMessage = "Il numero del giorno è obbligatorio.")]
        [Range(1, 365, ErrorMessage = "Il giorno deve essere compreso tra 1 e 365.")]
        public int Giorno { get; set; }

        [Required(ErrorMessage = "Il titolo è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il titolo non può superare i 100 caratteri.")]
        public string Titolo { get; set; }

        [Required(ErrorMessage = "La descrizione è obbligatoria.")]
        [StringLength(2000, ErrorMessage = "La descrizione non può superare i 2000 caratteri.")]
        public string Descrizione { get; set; }

        [ForeignKey("Itinerario")]
        [Required(ErrorMessage = "L'ID dell'itinerario è obbligatorio.")]
        public int IdItinerario { get; set; }

        public Itinerario Itinerario { get; set; }
    }
}
