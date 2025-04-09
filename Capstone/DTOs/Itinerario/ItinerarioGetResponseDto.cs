using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioGetResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
