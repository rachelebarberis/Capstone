using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.FasceDiPrezzo
{
    public class UpdateFasceDiPrezzoResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
