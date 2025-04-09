using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioFasciaPrezzoUpdateRequestDto
    {
        [Required]
        public int IdItinerarioFasciaPrezzo { get; set; }
        [Required]
        public int IdFasciaDiPrezzo { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Il prezzo deve essere maggiore di zero.")]
        public decimal Prezzo { get; set; }
    }
}
