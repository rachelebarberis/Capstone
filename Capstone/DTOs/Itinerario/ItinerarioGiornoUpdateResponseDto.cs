using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioGiornoUpdateResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
