using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
   


        public class Paese
        {
            [Key]  
            public int IdPaese { get; set; }

            [Required]  
            [StringLength(100)]  
            public required string Nome { get; set; }

 
            public List<Itinerario> Itinerari { get; set; }
        }
    }


