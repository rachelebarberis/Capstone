namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioGiornoRequestDto
    {
        public int IdItinerarioGiorno { get; set; }
        public int Giorno { get; set; }

        public string Titolo { get; set; }
        public string Descrizione { get; set; }
    }
}
