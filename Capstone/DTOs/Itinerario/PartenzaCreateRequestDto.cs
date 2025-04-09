using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class PartenzaCreateRequestDto
    {
        [Required]
        public DateOnly DataPartenza { get; set; }
    }
}
