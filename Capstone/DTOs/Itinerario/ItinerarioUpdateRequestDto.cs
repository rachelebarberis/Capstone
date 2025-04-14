using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioUpdateRequestDto
    {
        [Required]
        public int IdItinerario { get; set; }

        [Required]
            public string NomeItinerario { get; set; }
        [Required]
        public string ImmagineUrl { get; set; }

        [Required]
            [Range(1, int.MaxValue, ErrorMessage = "La durata deve essere maggiore di 0.")]
            public int Durata { get; set; }

            public List<ItinerarioGiornoUpdateRequestDto> Giorni { get; set; }
            public List<PartenzaUpdateRequestDto> Partenze { get; set; }
            public List<ItinerarioFasciaPrezzoUpdateRequestDto> ItinerarioFascePrezzo { get; set; }
        }

    
}
