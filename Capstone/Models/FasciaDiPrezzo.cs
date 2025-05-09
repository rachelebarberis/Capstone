﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Capstone.Models
{
    public class FasciaDiPrezzo
    {
        [Key]

        public int IdFasciaDiPrezzo { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri.")]
        public string Nome { get; set; }

        public List<ItinerarioFasciaPrezzo> Itinerari { get; set; } = new();
    }
}
