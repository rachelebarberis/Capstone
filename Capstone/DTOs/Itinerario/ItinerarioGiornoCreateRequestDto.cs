using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Itinerario
{
    public class ItinerarioGiornoCreateRequestDto
    {
        [Required]
        public int Giorno { get; set; }

        [Required]
        public string Descrizione { get; set; }
    }
}
