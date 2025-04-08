namespace Capstone.Models
{
    public class ItinerarioFasciaPrezzo
    {
        public int Id { get; set; }

        public int ItinerarioId { get; set; }
        public Itinerario Itinerario { get; set; }

        public int FasciaDiPrezzoId { get; set; }
        public FasciaDiPrezzo FasciaDiPrezzo { get; set; }

        public decimal Prezzo { get; set; }
    }
}
