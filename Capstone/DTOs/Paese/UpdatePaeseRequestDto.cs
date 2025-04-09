using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Paese
{
    public class UpdatePaeseRequestDto
    {

        [Required]
        [StringLength(100)]
        public required string Nome { get; set; }
    }
}
