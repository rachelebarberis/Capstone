using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioGiornoCreateResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
