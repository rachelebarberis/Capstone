using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Carrello
{
    public class CarrelloItemUpdateDto
    {
        [Required]
        public int IdItinerario { get; set; }  // ID dell'itinerario (estratto dal database)

        [Required]
        public int IdItinerarioFasciaPrezzo { get; set; } // ID della fascia di prezzo

        [Required]
        public int IdPartenza { get; set; }  // ID della partenza

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Il prezzo deve essere maggiore di zero.")]
        public decimal Prezzo { get; set; } // Prezzo dell'item

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La quantità deve essere almeno 1.")]
        public int Quantita { get; set; }  // Quantità dell'item
    }
}
