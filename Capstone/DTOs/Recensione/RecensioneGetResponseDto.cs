using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Recensione
{
    public class RecensioneGetResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
