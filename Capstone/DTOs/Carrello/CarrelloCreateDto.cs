using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.Carrello
{
    public class CarrelloCreateDto
    {
        [Required]
        public string UserEmail { get; set; } // ID dell'utente (presumibilmente sarà passato dal front-end tramite autenticazione)

        public List<CarrelloItemCreateDto> CarrelloItems { get; set; } = new();
    }
}
