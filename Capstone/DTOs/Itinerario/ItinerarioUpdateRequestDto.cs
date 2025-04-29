using System.ComponentModel.DataAnnotations;
using Capstone.DTOs.Paese;

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
        [Required]
        public List<ItinerarioGiornoUpdateRequestDto> Giorni { get; set; }
        [Required]
        public List<PartenzaUpdateRequestDto> Partenze { get; set; }
        [Required]
        public List<ItinerarioFasciaPrezzoUpdateRequestDto> ItinerarioFascePrezzo { get; set; }
        [Required]

        public PaeseRequestDto Paese { get; set; }
        }

    
}
