using System.ComponentModel.DataAnnotations;

namespace Capstone.DTOs.FasceDiPrezzo
{
    public class UpdateFasceDiPrezzoRequestDto
    {
        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri.")]
        public string Nome { get; set; }
    }
}
