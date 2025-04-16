namespace Capstone.DTOs.Carrello
{
    public class CarrelloItemDto
    {
        public int IdCarrelloItem { get; set; }
        public int IdItinerario { get; set; }
        public int IdItinerarioFasciaPrezzo { get; set; }
        public int IdPartenza { get; set; }
        public decimal Prezzo { get; set; }
        public int Quantita { get; set; }
        public decimal PrezzoTotale { get; set; }

        public string NomeItinerario { get; set; }
        public string ImmagineUrl { get; set; }

        public DateOnly DataPartenza { get; set; }
    }
}
