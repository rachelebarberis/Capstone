using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioGiornoResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
