﻿using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Capstone.Models
{
    public class Carrello
    {
        [Key]
        public int IdCarrello { get; set; }

        [Required(ErrorMessage = "L'ID utente è obbligatorio.")]
        [StringLength(450, ErrorMessage = "L'ID utente non può superare i 450 caratteri.")]


        public ApplicationUser User { get; set; }

        public string UserId { get; set; }


        public List<CarrelloItem> CarrelloItems { get; set; } = new();

        [NotMapped]
        public decimal Totale => CarrelloItems?.Sum(i => i.PrezzoTotale) ?? 0;
    }
}
