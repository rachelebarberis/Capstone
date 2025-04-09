using System.ComponentModel.DataAnnotations;
using Capstone.DTOs.Itinerario;

namespace Capstone.DTOs.Paese
{
    public class PaeseRequestDto
    {
        [Key]
        public int IdPaese { get; set; }

        [Required]
        [StringLength(100)]
        public required string Nome { get; set; }


    }
}
