using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioFasciaPrezzoCreateResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
