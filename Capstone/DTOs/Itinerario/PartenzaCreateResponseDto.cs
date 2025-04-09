using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class PartenzaCreateResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
