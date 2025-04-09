using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Paese
{
    public class PaeseResponseDto
    {
        [Required]
        public required string Message { get; set; }

        [Required]
       public ICollection<PaeseRequestDto> Paesi { get; set; }
    }
}
