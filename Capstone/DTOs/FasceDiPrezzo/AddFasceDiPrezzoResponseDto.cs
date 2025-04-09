using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.FasceDiPrezzo
{
    public class AddFasceDiPrezzoResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
