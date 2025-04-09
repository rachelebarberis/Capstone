using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioUpdateResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
