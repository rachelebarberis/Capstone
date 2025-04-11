using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Recensione
{
    public class RecensioneUpdateRequestDto
    {
        [Required]
        public int IdRecensione { get; set; }

        [Required]
        public string Commento { get; set; }



        [Range(1, 5)]
        public int Valutazione { get; set; }

        // opzionale
        public IFormFile? ImgUser { get; set; }
    }
}
