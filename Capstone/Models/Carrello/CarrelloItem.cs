using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Models.Carrello
{
    public class CarrelloItem
    {
        [Key]
        public int IdCarrelloItem { get; set; }

        [ForeignKey("Itinerario")]
        [Required(ErrorMessage = "L'ID dell'itinerario è obbligatorio.")]
        public int IdItinerario { get; set; }
        public Itinerario Itinerario { get; set; }

        [ForeignKey("ItinerarioFasciaPrezzo")]
        [Required(ErrorMessage = "L'ID della fascia di prezzo è obbligatorio.")]
        public int IdItinerarioFasciaPrezzo { get; set; }
        public ItinerarioFasciaPrezzo ItinerarioFasciaPrezzo { get; set; }

        [ForeignKey("Partenza")]
        [Required(ErrorMessage = "L'ID della partenza è obbligatorio.")]
        public int IdPartenza { get; set; }
        public Partenza Partenza { get; set; }

        [Required(ErrorMessage = "Il prezzo è obbligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Il prezzo deve essere maggiore di zero.")]
        public decimal Prezzo { get; set; }

        [Required(ErrorMessage = "La quantità è obbligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La quantità deve essere almeno 1.")]
        public int Quantita { get; set; }

        [NotMapped]
        public decimal PrezzoTotale => Prezzo * Quantita;

        [ForeignKey("Carrello")]
        [Required(ErrorMessage = "L'ID del carrello è obbligatorio.")]
        public int IdCarrello { get; set; }
        public Carrello Carrello { get; set; }
    }
}
