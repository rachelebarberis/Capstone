using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Recensione
{
    public class RecensioneUpdateResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
