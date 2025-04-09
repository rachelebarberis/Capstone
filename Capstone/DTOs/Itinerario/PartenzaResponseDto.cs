using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class PartenzaResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
