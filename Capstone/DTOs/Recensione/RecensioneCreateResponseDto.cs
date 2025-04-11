using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Recensione
{
    public class RecensioneCreateResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
