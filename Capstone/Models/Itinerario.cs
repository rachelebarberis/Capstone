using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models
{
    public class Itinerario
    {
        [Key]
        public int IdItinerario { get; set; }

        [Required(ErrorMessage = "Il nome dell'itinerario è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il nome dell'itinerario non può superare i 100 caratteri.")]
        public string NomeItinerario { get; set; }

        [Required(ErrorMessage = "La durata è obbligatoria.")]
        [Range(1, 365, ErrorMessage = "La durata deve essere compresa tra 1 e 365 giorni.")]
        public int Durata { get; set; }

        [Url(ErrorMessage = "Inserisci un URL valido.")]
        public string ImmagineUrl { get; set; }

        [ForeignKey("Paese")]
        [Required(ErrorMessage = "Il Paese è obbligatorio.")]
        public int PaeseId { get; set; }

        public Paese Paese { get; set; }

        public List<ItinerarioGiorno> ItinerarioGiorni { get; set; } = new();

        public List<ItinerarioFasciaPrezzo> FasceDiPrezzo { get; set; } = new();

        public List<Partenza> Partenze { get; set; } = new();

        public List<Recensione> Recensioni { get; set; } = new();
    }
}
