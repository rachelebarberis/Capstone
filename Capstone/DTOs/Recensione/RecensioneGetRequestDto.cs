using Capstone.Models;

namespace Capstone.DTOs.Recensione
{
    public class RecensioneGetRequestDto
    {
        public int IdRecensione { get; set; }
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public string Testo { get; set; }
        public int Valutazione { get; set; }
        public int IdItinerario { get; set; }
     
    }
}
