using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioFasciaPrezzoUpdateResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
