namespace Capstone.DTOs.Recensione
{
    public class RecensioneCreateRequestDto
    {
        public string UserId { get; set; }
        public string Testo { get; set; }
        public int Valutazione { get; set; }
        public int IdItinerario { get; set; }
    }
}
