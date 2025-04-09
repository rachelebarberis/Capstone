using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Paese
{
    public class AddPaeseResponseDto
    {

        [Required]
        public required string Message { get; set; }
    }
}
