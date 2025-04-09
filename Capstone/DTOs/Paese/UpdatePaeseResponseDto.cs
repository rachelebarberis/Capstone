using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Paese
{
    public class UpdatePaeseResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
