namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioGetRequestDto
    {
        public int IdItinerario { get; set; }
        public string NomeItinerario { get; set; }
        public int Durata { get; set; }
        public List<ItinerarioGiornoRequestDto> Giorni { get; set; }
        public List<PartenzaRequestDto> Partenze { get; set; }
        public List<ItinerarioFasciaPrezzoRequestDto> ItinerarioFascePrezzo { get; set; }
    }
}
