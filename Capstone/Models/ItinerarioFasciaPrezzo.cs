using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class ItinerarioFasciaPrezzo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdItinerarioFasciaPrezzo { get; set; }

        [Required]
        public int IdItinerario { get; set; }

        [ForeignKey("IdItinerario")]
        public Itinerario Itinerario { get; set; }

        [Required]
        public int IdFasciaDiPrezzo { get; set; }

        [ForeignKey("IdFasciaDiPrezzo")]
        public FasciaDiPrezzo FasciaDiPrezzo { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Prezzo { get; set; }
    }
}

