using Capstone.Models;
using Capstone.DTOs.Carrello;
using Microsoft.EntityFrameworkCore;
using System;
using Capstone.Data;

namespace Capstone.Services
{
    public class CarrelloService
    {
        private readonly ApplicationDbContext _context;

        public CarrelloService(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<CarrelloDto> GetCarrelloByEmailAsync(string email)
        {
       
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            var carrello = await _context.Carrelli
                .Include(c => c.CarrelloItems)
                    .ThenInclude(ci => ci.Itinerario) 
                .Include(c => c.CarrelloItems)
                    .ThenInclude(ci => ci.ItinerarioFasciaPrezzo) 
                .Include(c => c.CarrelloItems)
                    .ThenInclude(ci => ci.Partenza) 
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (carrello == null) return null;

         
            return new CarrelloDto
            {
                IdCarrello = carrello.IdCarrello,
                UserId = carrello.UserId,
                Totale = carrello.Totale,
                CarrelloItems = carrello.CarrelloItems.Select(item => new CarrelloItemDto
                {
                    IdCarrelloItem = item.IdCarrelloItem,
                    IdItinerario = item.IdItinerario,
                    IdItinerarioFasciaPrezzo = item.IdItinerarioFasciaPrezzo,
                    IdPartenza = item.IdPartenza,
                    Prezzo = item.Prezzo,
                    Quantita = item.Quantita,
                    PrezzoTotale = item.PrezzoTotale,

                    NomeItinerario = item.Itinerario?.NomeItinerario,
                    ImmagineUrl = item.Itinerario?.ImmagineUrl,
                    DataPartenza = item.Partenza.DataPartenza
                }).ToList()
            };
        }

        public async Task<CarrelloDto> CreateCarrelloAsync(CarrelloCreateDto dto)
        {
         
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.UserEmail);
            if (user == null)
                return null;

            var carrello = await _context.Carrelli
                .Include(c => c.CarrelloItems)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (carrello == null)
            {
                carrello = new Carrello
                {
                    UserId = user.Id,
                    CarrelloItems = new List<CarrelloItem>()
                };
                _context.Carrelli.Add(carrello);
            }

            foreach (var itemDto in dto.CarrelloItems)
            {
                carrello.CarrelloItems.Add(new CarrelloItem
                {
                    IdItinerario = itemDto.IdItinerario,
                    IdItinerarioFasciaPrezzo = itemDto.IdItinerarioFasciaPrezzo,
                    IdPartenza = itemDto.IdPartenza,
                    Prezzo = itemDto.Prezzo,
                    Quantita = itemDto.Quantita
                });
            }

            await _context.SaveChangesAsync();
            return await GetCarrelloByEmailAsync(dto.UserEmail);
        }

        public async Task<CarrelloDto> UpdateCarrelloItemAsync(int itemId, CarrelloItemUpdateDto dto)
        {
            var item = await _context.CarrelloItems
                .Include(i => i.Carrello)
                .FirstOrDefaultAsync(i => i.IdCarrelloItem == itemId);

            if (item == null)
                return null;

            item.IdItinerario = dto.IdItinerario;
            item.IdItinerarioFasciaPrezzo = dto.IdItinerarioFasciaPrezzo;
            item.IdPartenza = dto.IdPartenza;
            item.Prezzo = dto.Prezzo;
            item.Quantita = dto.Quantita;

            await _context.SaveChangesAsync();

            return await GetCarrelloByEmailAsync(item.Carrello.User.Email);  
        }

        public async Task<bool> RemoveItemAsync(int itemId)
        {
            var item = await _context.CarrelloItems.FindAsync(itemId);
            if (item == null) return false;

            _context.CarrelloItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCarrelloAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            var carrello = await _context.Carrelli
                .Include(c => c.CarrelloItems)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (carrello == null) return false;

            _context.CarrelloItems.RemoveRange(carrello.CarrelloItems);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
