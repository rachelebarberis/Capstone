namespace Capstone.Models
{
    public class ItinerarioGiorno
    {
        public int Id { get; set; }
        public int Giorno { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }

        public int ItinerarioId { get; set; }
        public Itinerario Itinerario { get; set; }
    }
}
