using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class PartenzaUpdateResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
