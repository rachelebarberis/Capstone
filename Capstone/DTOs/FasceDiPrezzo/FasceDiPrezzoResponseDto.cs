using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.FasceDiPrezzo
{
    public class FasceDiPrezzoResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
