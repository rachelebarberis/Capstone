using Capstone.Models;

namespace Capstone.Models
{
    public class FasciaDiPrezzo
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<ItinerarioFasciaPrezzo> Itinerari { get; set; }
    }
}
