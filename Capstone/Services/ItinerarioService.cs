using Capstone.Data;
using Capstone.DTOs.Itinerario;
using Capstone.DTOs.Paese;
using Capstone.Models;
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
               .Include(i => i.Paese)
                .Include(i => i.ItinerarioFascePrezzo)
                    .ThenInclude(fp => fp.FasciaDiPrezzo)
                .Include(i => i.ItinerarioGiorni)
                .Include(i => i.Partenze)
                .ToListAsync();

            return (List<ItinerarioGetRequestDto>)itinerari.Select(itinerario => new ItinerarioGetRequestDto
            {
                IdItinerario = itinerario.IdItinerario,
                NomeItinerario = itinerario.NomeItinerario,
                ImmagineUrl = itinerario.ImmagineUrl,
                Paese = new PaeseRequestDto
                {
                    IdPaese = itinerario.Paese.IdPaese,
                    Nome = itinerario.Paese.Nome
                },
                Durata = itinerario.Durata,
                Giorni = itinerario.ItinerarioGiorni.Select(g => new ItinerarioGiornoRequestDto
                {
                IdItinerarioGiorno= g.IdItinerarioGiorno,
                    Giorno = g.Giorno,
                   Titolo = g.Titolo,
                    Descrizione = g.Descrizione
                }).ToList(),
                Partenze = itinerario.Partenze.Select(p => new PartenzaRequestDto
                {
                    IdPartenza = p.IdPartenza, // Assicurati che sia "Id", non "IdPartenza"
                    DataPartenza = p.DataPartenza,
                    Stato = p.Stato,
                }).ToList(),
                ItinerarioFascePrezzo = itinerario.ItinerarioFascePrezzo.Select(fp => new ItinerarioFasciaPrezzoRequestDto
                {
                    IdItinerarioFasciaPrezzo = fp.IdItinerarioFasciaPrezzo, // Assicurati che sia "Id", non "IdItinerarioFasciaPrezzo"
                    IdFasciaDiPrezzo = fp.IdFasciaDiPrezzo,
                    Prezzo = fp.Prezzo
                }).ToList()
            }).ToList();
        }

        public async Task<ItinerarioGetRequestDto> GetItinerarioAsync(int id)
        {
            var itinerario = await _context.Itinerari
                .Include(i => i.Paese)
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
                ImmagineUrl = itinerario.ImmagineUrl,
                Paese = new PaeseRequestDto
                {
                    IdPaese = itinerario.Paese.IdPaese,
                    Nome = itinerario.Paese.Nome
                },
                Durata = itinerario.Durata,
                Giorni = itinerario.ItinerarioGiorni.Select(g => new ItinerarioGiornoRequestDto
                {
                
                    Giorno = g.Giorno,
                    Titolo = g.Titolo,
                    Descrizione = g.Descrizione
                }).ToList(),
                Partenze = itinerario.Partenze.Select(p => new PartenzaRequestDto
                {
                    IdPartenza = p.IdPartenza,
   
                    DataPartenza = p.DataPartenza,
                    Stato = p.Stato,
                }).ToList(),
                ItinerarioFascePrezzo = itinerario.ItinerarioFascePrezzo.Select(fp => new ItinerarioFasciaPrezzoRequestDto
                {
                    IdItinerarioFasciaPrezzo = fp.IdItinerarioFasciaPrezzo,
                    IdFasciaDiPrezzo = fp.IdFasciaDiPrezzo,
                    Prezzo = fp.Prezzo
                }).ToList()
            };
        }

        public async Task<ItinerarioCreateRequestDto> CreateItinerarioAsync(ItinerarioCreateRequestDto itinerarioCreateRequestDto)
        {
            // Prendi il Paese corrispondente all'IdPaese
            var paese = await _context.Paesi.FindAsync(itinerarioCreateRequestDto.Paese.IdPaese);
            if (paese == null)
            {
                throw new Exception("Paese non trovato.");
            }

            var itinerario = new Itinerario
            {
                NomeItinerario = itinerarioCreateRequestDto.NomeItinerario,
                Durata = itinerarioCreateRequestDto.Durata,
                ImmagineUrl = itinerarioCreateRequestDto.ImmagineUrl,
                Paese = paese,
                ItinerarioGiorni = itinerarioCreateRequestDto.ItinerarioGiorni.Select(g => new ItinerarioGiorno
                {
                    Giorno = g.Giorno,
                    Titolo = g.Titolo,
                    Descrizione = g.Descrizione
                }).ToList(),
                Partenze = itinerarioCreateRequestDto.Partenze.Select(p => new Partenza
                {
                    DataPartenza = p.DataPartenza,
                    Stato = p.Stato,
                }).ToList(),
                ItinerarioFascePrezzo = itinerarioCreateRequestDto.ItinerarioFascePrezzo.Select(fp => new ItinerarioFasciaPrezzo
                {
                    IdFasciaDiPrezzo = fp.IdFasciaDiPrezzo,
                    Prezzo = fp.Prezzo
                }).ToList()
            };



            _context.Itinerari.Add(itinerario);
            await _context.SaveChangesAsync();


            return new ItinerarioCreateRequestDto
            {
                NomeItinerario = itinerario.NomeItinerario,
                Durata = itinerario.Durata,
                ImmagineUrl = itinerario.ImmagineUrl,
                Paese = new PaeseRequestDto
                {
                    IdPaese = itinerario.Paese.IdPaese,
                    Nome = itinerario.Paese.Nome
                },
                ItinerarioGiorni = itinerario.ItinerarioGiorni.Select(g => new ItinerarioGiornoCreateRequestDto
                {
                    Giorno = g.Giorno,
                    Titolo = g.Titolo,
                    Descrizione = g.Descrizione
                }).ToList(),
                Partenze = itinerario.Partenze.Select(p => new PartenzaCreateRequestDto
                {

                    DataPartenza = p.DataPartenza,
                    Stato= p.Stato,
                }).ToList(),
                ItinerarioFascePrezzo = itinerario.ItinerarioFascePrezzo.Select(fp => new ItinerarioFasciaPrezzoCreateRequestDto
                {

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
            itinerario.ImmagineUrl = itinerarioUpdateRequestDto.ImmagineUrl;

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
                ImmagineUrl = itinerario.ImmagineUrl,
                Durata = itinerario.Durata,
                Giorni = itinerario.ItinerarioGiorni.Select(g => new ItinerarioGiornoUpdateRequestDto
                {
         
                    Giorno = g.Giorno,
                    Titolo = g.Titolo,
                    Descrizione = g.Descrizione
                }).ToList(),
                Partenze = itinerario.Partenze.Select(p => new PartenzaUpdateRequestDto
                {
                    IdPartenza = p.IdPartenza,
                
                    DataPartenza = p.DataPartenza,
                    Stato = p.Stato,
                }).ToList(),
                ItinerarioFascePrezzo = itinerario.ItinerarioFascePrezzo.Select(fp => new ItinerarioFasciaPrezzoUpdateRequestDto
                {
                   
               
                    Prezzo = fp.Prezzo
                }).ToList()
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
