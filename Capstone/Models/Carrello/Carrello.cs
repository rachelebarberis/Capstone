namespace Capstone.Models.Carrello
{
    public class Carrello
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public List<CarrelloItem> CarrelloItems { get; set; } = new();

        public decimal Totale => CarrelloItems.Sum(i => i.PrezzoTotale);
    }
}
