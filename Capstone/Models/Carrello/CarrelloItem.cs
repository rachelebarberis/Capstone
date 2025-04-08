namespace Capstone.Models.Carrello
{
    public class CarrelloItem
    {
        public int Id { get; set; }

        public int ItinerarioId { get; set; }
        public Itinerario Itinerario { get; set; }

        public int FasciaDiPrezzoId { get; set; }
        public FasciaDiPrezzo FasciaDiPrezzo { get; set; }

        public decimal Prezzo { get; set; } 
        public int Quantita { get; set; }

        public decimal PrezzoTotale => Prezzo * Quantita;

        public int CarrelloId { get; set; }
        public Carrello Carrello { get; set; }
    }
}
