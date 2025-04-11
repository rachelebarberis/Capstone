namespace Capstone.DTOs.Carrello
{
    public class CarrelloDto
    {
        public int IdCarrello { get; set; }
        public string UserId { get; set; }
        public decimal Totale { get; set; }
        public List<CarrelloItemDto> CarrelloItems { get; set; }
    }
}
