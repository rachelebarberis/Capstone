namespace Capstone.DTOs.Itinerario
{
    public class PartenzaUpdateRequestDto
    {
        public int IdPartenza { get; set; }

        public DateOnly DataPartenza { get; set; }

        public string Stato { get; set; }   
    }
}
