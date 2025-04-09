using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models
{
    public class Partenza
    {
        [Key]
        public int IdPartenza { get; set; }

        [ForeignKey("Itinerario")]
        [Required(ErrorMessage = "L'ID dell'itinerario è obbligatorio.")]
        public int IdItinerario { get; set; }

        public Itinerario Itinerario { get; set; }

        [Required(ErrorMessage = "La data di partenza è obbligatoria.")]
        [DataType(DataType.Date)]
        public DateOnly DataPartenza { get; set; }

        [Required(ErrorMessage = "Il numero di posti disponibili è obbligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "I posti disponibili devono essere maggiori o uguali a zero.")]
        public int PostiDisponibili { get; set; }

        [Required(ErrorMessage = "Lo stato della partenza è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Lo stato non può superare i 50 caratteri.")]
        public string Stato { get; set; } // Esempi: "Disponibile", "Sold Out"
    }
}
