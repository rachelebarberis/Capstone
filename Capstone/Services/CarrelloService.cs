using Capstone.Data;
using Capstone.DTOs.Carrello;
using Capstone.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Services
{
    public class CarrelloService
    {
        private readonly ApplicationDbContext _context;

        public CarrelloService(ApplicationDbContext context)
        {
            _context = context;
        }
        

        // Crea un nuovo carrello per un utente, aggiungendo gli articoli al carrello
        public async Task<CarrelloDto> CreaCarrelloAsync(CarrelloCreateDto carrelloCreateDto)
        {
            var carrello = new Carrello
            {
                UserId = carrelloCreateDto.UserId,
                CarrelloItems = new List<CarrelloItem>()
            };

            _context.Carrelli.Add(carrello);
            await _context.SaveChangesAsync();

            // Aggiungere gli articoli al carrello appena creato
            foreach (var item in carrelloCreateDto.CarrelloItems)
            {
                var carrelloItem = new CarrelloItem
                {
                    IdItinerario = item.IdItinerario,
                    IdItinerarioFasciaPrezzo = item.IdItinerarioFasciaPrezzo,
                    IdPartenza = item.IdPartenza,
                    Prezzo = item.Prezzo,
                    Quantita = item.Quantita,
                    IdCarrello = carrello.IdCarrello
                };

                carrello.CarrelloItems.Add(carrelloItem);
            }

            await _context.SaveChangesAsync();

            return new CarrelloDto
            {
                IdCarrello = carrello.IdCarrello,
                UserId = carrello.UserId,
                Totale = carrello.Totale,
                CarrelloItems = carrello.CarrelloItems.Select(ci => new CarrelloItemDto
                {
                    IdCarrelloItem = ci.IdCarrelloItem,
                    IdItinerario = ci.IdItinerario,
                    IdItinerarioFasciaPrezzo = ci.IdItinerarioFasciaPrezzo,
                    IdPartenza = ci.IdPartenza,
                    Prezzo = ci.Prezzo,
                    Quantita = ci.Quantita,
                    PrezzoTotale = ci.PrezzoTotale
                }).ToList()
            };
        }

        // Aggiungi un nuovo articolo al carrello
        public async Task<CarrelloItemDto> AggiungiAlCarrelloAsync(string userId, CarrelloItemCreateDto carrelloItemCreateDto)
        {
            var carrello = await _context.Carrelli
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (carrello == null)
            {
                return null;
            }

            var carrelloItem = new CarrelloItem
            {
                IdItinerario = carrelloItemCreateDto.IdItinerario,
                IdItinerarioFasciaPrezzo = carrelloItemCreateDto.IdItinerarioFasciaPrezzo,
                IdPartenza = carrelloItemCreateDto.IdPartenza,
                Prezzo = carrelloItemCreateDto.Prezzo,
                Quantita = carrelloItemCreateDto.Quantita,
                IdCarrello = carrello.IdCarrello
            };

            carrello.CarrelloItems.Add(carrelloItem);
            await _context.SaveChangesAsync();

            return new CarrelloItemDto
            {
                IdCarrelloItem = carrelloItem.IdCarrelloItem,
                IdItinerario = carrelloItem.IdItinerario,
                IdItinerarioFasciaPrezzo = carrelloItem.IdItinerarioFasciaPrezzo,
                IdPartenza = carrelloItem.IdPartenza,
                Prezzo = carrelloItem.Prezzo,
                Quantita = carrelloItem.Quantita,
                PrezzoTotale = carrelloItem.PrezzoTotale
            };
        }

        // Aggiorna un articolo esistente nel carrello
        public async Task<CarrelloItemDto> AggiornaCarrelloItemAsync(int idCarrelloItem, CarrelloItemUpdateDto carrelloItemUpdateDto)
        {
            var carrelloItem = await _context.CarrelloItems
                .FirstOrDefaultAsync(ci => ci.IdCarrelloItem == idCarrelloItem);

            if (carrelloItem == null)
            {
                return null;
            }

            carrelloItem.IdItinerario = carrelloItemUpdateDto.IdItinerario;
            carrelloItem.IdItinerarioFasciaPrezzo = carrelloItemUpdateDto.IdItinerarioFasciaPrezzo;
            carrelloItem.IdPartenza = carrelloItemUpdateDto.IdPartenza;
            carrelloItem.Prezzo = carrelloItemUpdateDto.Prezzo;
            carrelloItem.Quantita = carrelloItemUpdateDto.Quantita;

            await _context.SaveChangesAsync();

            return new CarrelloItemDto
            {
                IdCarrelloItem = carrelloItem.IdCarrelloItem,
                IdItinerario = carrelloItem.IdItinerario,
                IdItinerarioFasciaPrezzo = carrelloItem.IdItinerarioFasciaPrezzo,
                IdPartenza = carrelloItem.IdPartenza,
                Prezzo = carrelloItem.Prezzo,
                Quantita = carrelloItem.Quantita,
                PrezzoTotale = carrelloItem.PrezzoTotale
            };
        }

        // Rimuovi un articolo dal carrello
        public async Task<bool> RimuoviDalCarrelloAsync(int idCarrelloItem)
        {
            var carrelloItem = await _context.CarrelloItems
                .FirstOrDefaultAsync(ci => ci.IdCarrelloItem == idCarrelloItem);

            if (carrelloItem == null)
            {
                return false;
            }

            _context.CarrelloItems.Remove(carrelloItem);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}
