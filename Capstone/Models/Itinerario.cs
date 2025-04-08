namespace Capstone.Models
{
    public class Itinerario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Durata { get; set; }

        public int PaeseId { get; set; }
        public Paese Paese { get; set; }

        public List<ItinerarioGiorno> ItinerarioGiorni { get; set; }
        public List<ItinerarioFasciaPrezzo> FasceDiPrezzo { get; set; }
    }
}
