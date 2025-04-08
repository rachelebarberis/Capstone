namespace Capstone.Models
{
    public class Paese
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<Itinerario> Itinerari { get; set; }
    }
}
