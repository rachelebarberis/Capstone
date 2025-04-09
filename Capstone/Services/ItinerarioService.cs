using Capstone.Data;
using Capstone.Models;
using Capstone.DTOs.Itinerario;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Services
{
    public class ItinerarioService 
    {
        private readonly ApplicationDbContext _context;

        public ItinerarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ItinerarioGetRequestDto>> GetItinerariAsync()
        {
            var itinerari = await _context.Itinerari
                .Include(i => i.ItinerarioFascePrezzo)
                    .ThenInclude(fp => fp.FasciaDiPrezzo)
                .Include(i => i.ItinerarioGiorni)
                .Include(i => i.Partenze)
                .ToListAsync();

            return itinerari.Select(itinerario => new ItinerarioGetRequestDto
            {
                IdItinerario = itinerario.IdItinerario,
                NomeItinerario = itinerario.NomeItinerario,
                Durata = itinerario.Durata,
                Giorni = itinerario.ItinerarioGiorni.Select(g => new ItinerarioGiornoRequestDto
                {
                    IdItinerarioGiorno = g.IdItinerarioGiorno, // Assicurati che sia "Id", non "IdItinerarioGiorno"
                    Giorno = g.Giorno,
                    Descrizione = g.Descrizione
                }).ToList(),
                Partenze = itinerario.Partenze.Select(p => new PartenzaRequestDto
                {
                    IdPartenza = p.IdPartenza, // Assicurati che sia "Id", non "IdPartenza"
                    DataPartenza = p.DataPartenza
                }).ToList(),
                ItinerarioFascePrezzo = itinerario.ItinerarioFascePrezzo.Select(fp => new ItinerarioFasciaPrezzoRequestDto
                {
                    IdItinerarioFasciaPrezzo = fp.IdItinerarioFasciaPrezzo, // Assicurati che sia "Id", non "IdItinerarioFasciaPrezzo"
                    IdFasciaDiPrezzo = fp.IdFasciaDiPrezzo,
                    Prezzo = fp.Prezzo
                }).ToList()
            }).ToList(); // <-- Mancava questo
        }

        public async Task<ItinerarioGetRequestDto> GetItinerarioAsync(int id)
        {
            var itinerario = await _context.Itinerari
                .Include(i => i.ItinerarioFascePrezzo)
                .ThenInclude(fp => fp.FasciaDiPrezzo)
                .Include(i => i.ItinerarioGiorni)
                .Include(i => i.Partenze)
                .FirstOrDefaultAsync(i => i.IdItinerario == id);

            if (itinerario == null)
            {
                return null;
            }

            return new ItinerarioGetRequestDto
            {
                IdItinerario = itinerario.IdItinerario,
                NomeItinerario = itinerario.NomeItinerario,
                Durata = itinerario.Durata,
                Giorni = itinerario.ItinerarioGiorni.Select(g => new ItinerarioGiornoRequestDto
                {
                    IdItinerarioGiorno = g.IdItinerarioGiorno,
                    Giorno = g.Giorno,
                    Descrizione = g.Descrizione
                }).ToList(),
                Partenze = itinerario.Partenze.Select(p => new PartenzaRequestDto
                {
                    IdPartenza = p.IdPartenza,
   
                    DataPartenza = p.DataPartenza
                }).ToList(),
                ItinerarioFascePrezzo = itinerario.ItinerarioFascePrezzo.Select(fp => new ItinerarioFasciaPrezzoRequestDto
                {
                    IdItinerarioFasciaPrezzo = fp.IdItinerarioFasciaPrezzo,
                    IdFasciaDiPrezzo = fp.IdFasciaDiPrezzo,
                    Prezzo = fp.Prezzo
                }).ToList()
            };
        }

        public async Task<ItinerarioUpdateRequestDto> UpdateItinerarioAsync(int id, ItinerarioUpdateRequestDto itinerarioUpdateRequestDto)
            
        {
            var itinerario = await _context.Itinerari
                .Include(i => i.ItinerarioFascePrezzo)
                .FirstOrDefaultAsync(i => i.IdItinerario == id);

            if (itinerario == null)
            {
                return null;
            }

            itinerario.NomeItinerario = itinerarioUpdateRequestDto.NomeItinerario;
            itinerario.Durata = itinerarioUpdateRequestDto.Durata;

            // Aggiornamento delle fasce di prezzo
            foreach (var fasciaDto in itinerarioUpdateRequestDto.ItinerarioFascePrezzo)
            {
                var fasciaPrezzo = itinerario.ItinerarioFascePrezzo
                    .FirstOrDefault(fp => fp.IdItinerarioFasciaPrezzo == fasciaDto.IdItinerarioFasciaPrezzo);
                if (fasciaPrezzo != null)
                {
                    fasciaPrezzo.Prezzo = fasciaDto.Prezzo;
                }
            }

            await _context.SaveChangesAsync();

            return new ItinerarioUpdateRequestDto
            {
               
                NomeItinerario = itinerario.NomeItinerario,
                Durata = itinerario.Durata,
                Giorni = itinerario.ItinerarioGiorni.Select(g => new ItinerarioGiornoUpdateRequestDto
                {
                    IdItinerarioGiorno = g.IdItinerarioGiorno,
                    Giorno = g.Giorno,
                    Descrizione = g.Descrizione
                }).ToList(),
                Partenze = itinerario.Partenze.Select(p => new PartenzaUpdateRequestDto
                {
                    IdPartenza = p.IdPartenza,
                
                    DataPartenza = p.DataPartenza
                }).ToList(),
                ItinerarioFascePrezzo = itinerario.ItinerarioFascePrezzo.Select(fp => new ItinerarioFasciaPrezzoUpdateRequestDto
                {
                    IdItinerarioFasciaPrezzo = fp.IdItinerarioFasciaPrezzo,
               
                    Prezzo = fp.Prezzo
                }).ToList()
            };
        }

        public async Task<ItinerarioCreateRequestDto> CreateItinerarioAsync(ItinerarioCreateRequestDto itinerarioCreateRequestDto)
        {
            var itinerario = new Itinerario
            {
                NomeItinerario = itinerarioCreateRequestDto.NomeItinerario,
                Durata = itinerarioCreateRequestDto.Durata
            };

            _context.Itinerari.Add(itinerario);
            await _context.SaveChangesAsync();

            return new ItinerarioCreateRequestDto
            {
           
                NomeItinerario = itinerario.NomeItinerario,
                Durata = itinerario.Durata
            };
        }

        public async Task DeleteItinerarioAsync(int id)
        {
            var itinerario = await _context.Itinerari
                .Include(i => i.ItinerarioFascePrezzo)
                .FirstOrDefaultAsync(i => i.IdItinerario == id);

            if (itinerario != null)
            {
                _context.Itinerari.Remove(itinerario);
                await _context.SaveChangesAsync();
            }
        }
    }

}
