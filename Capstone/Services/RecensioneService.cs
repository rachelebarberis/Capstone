using Capstone.Data;
using Capstone.DTOs.Recensione;
using Capstone.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Services
{
    public class RecensioneService
    {
        private readonly ApplicationDbContext _context;

        public RecensioneService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RecensioneGetRequestDto>> GetRecensioniAll()
        {
            var recensioni = await _context.Recensioni
                .Include(r => r.Itinerario)
                .ToListAsync();

            return recensioni.Select(r => new RecensioneGetRequestDto
            {
                IdRecensione = r.IdRecensione,
                UserId = r.UserId,
                Testo = r.Testo,
                Valutazione = r.Valutazione,
                IdItinerario = r.IdItinerario,
                User = r.User,  
            }).ToList();
        }

        // GET: Recensioni per itinerario
        public async Task<List<RecensioneGetRequestDto>> GetRecensioniByItinerarioAsync(int idItinerario)
        {
            var recensioni = await _context.Recensioni
                .Where(r => r.IdItinerario == idItinerario)
                .Include(r => r.Itinerario)
                .ToListAsync();

            return recensioni.Select(r => new RecensioneGetRequestDto
            {
                IdRecensione = r.IdRecensione,
                UserId = r.UserId,
                Testo = r.Testo,
                Valutazione = r.Valutazione,
                IdItinerario = r.IdItinerario,
                User = r.User
            }).ToList();
        }

        // GET: Recensione per Id
        public async Task<RecensioneGetRequestDto> GetRecensioneByIdAsync(int id)
        {
            var recensione = await _context.Recensioni
                .Include(r => r.Itinerario)
                .FirstOrDefaultAsync(r => r.IdRecensione == id);

            if (recensione == null)
            {
                return null;
            }

            return new RecensioneGetRequestDto
            {
                IdRecensione = recensione.IdRecensione,
                UserId = recensione.UserId,
                Testo = recensione.Testo,
                Valutazione = recensione.Valutazione,
                IdItinerario = recensione.IdItinerario,
                User = recensione.User,
            };
        }

        // POST: Crea una recensione
        public async Task<RecensioneCreateRequestDto> CreateRecensioneAsync(RecensioneCreateRequestDto dto)
        {
            var recensione = new Recensione
            {
                UserId = dto.UserId,
                Testo = dto.Testo,
                Valutazione = dto.Valutazione,
                IdItinerario = dto.IdItinerario
            };

            _context.Recensioni.Add(recensione);
            await _context.SaveChangesAsync();

            return dto;
        }

        // PUT: Aggiorna una recensione
        public async Task<RecensioneUpdateRequestDto> UpdateRecensioneAsync(int id, RecensioneUpdateRequestDto dto)
        {
            var recensione = await _context.Recensioni
                .FirstOrDefaultAsync(r => r.IdRecensione == id);

            if (recensione == null)
            {
                return null;
            }

            recensione.Testo = dto.Testo;
            recensione.Valutazione = dto.Valutazione;

            await _context.SaveChangesAsync();

            return dto;
        }

        // DELETE: Elimina una recensione
        public async Task DeleteRecensioneAsync(int id)
        {
            var recensione = await _context.Recensioni
                .FirstOrDefaultAsync(r => r.IdRecensione == id);

            if (recensione != null)
            {
                _context.Recensioni.Remove(recensione);
                await _context.SaveChangesAsync();
            }
        }
    }
}

