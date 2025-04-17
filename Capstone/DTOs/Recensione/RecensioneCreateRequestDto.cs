using System.ComponentModel.DataAnnotations;
using Capstone.Models;

namespace Capstone.DTOs.Recensione
{
    public class RecensioneCreateRequestDto
    {
        [Required]
        public string Commento { get; set; }

        [Range(1, 5)]
        public int Valutazione { get; set; }

        public string? UserId { get; set; }     

        [Required]
        public int IdItinerario { get; set; }

        // opzionale
        public IFormFile? ImgUser { get; set; }
    }
}
