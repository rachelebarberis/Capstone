using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioFasciaPrezzoResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
