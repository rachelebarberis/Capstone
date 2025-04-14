using System.ComponentModel.DataAnnotations;
using Capstone.DTOs.Paese;

namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioCreateRequestDto
    {
        [Required]
        public string NomeItinerario { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La durata deve essere maggiore di 0.")]
        public int Durata { get; set; }

        [Required]
        public string ImmagineUrl { get; set; }
        public List<ItinerarioGiornoCreateRequestDto> ItinerarioGiorni { get; set; }




        public List<PartenzaCreateRequestDto> Partenze { get; set; }

        public PaeseRequestDto Paese { get; set; }

        public List<ItinerarioFasciaPrezzoCreateRequestDto> ItinerarioFascePrezzo { get; set; }
    }
}
