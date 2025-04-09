using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioCreateRequestDto
    {
        [Required]
        public string NomeItinerario { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La durata deve essere maggiore di 0.")]
        public int Durata { get; set; }

        public List<ItinerarioGiornoCreateRequestDto> Giorni { get; set; }

     
        public List<PartenzaCreateRequestDto> Partenze { get; set; }

        public List<ItinerarioFasciaPrezzoCreateRequestDto> ItinerarioFasceDiPrezzo { get; set; }
    }
}
