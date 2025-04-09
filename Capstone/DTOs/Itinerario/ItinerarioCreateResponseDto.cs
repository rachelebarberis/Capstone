using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioCreateResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
