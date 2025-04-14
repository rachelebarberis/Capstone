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
                .Include(i => i.Giorni)
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
                Giorni = itinerario.Giorni.Select(g => new ItinerarioGiornoRequestDto
                {
                    IdItinerarioGiorno = g.IdItinerarioGiorno,
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
                .Include(i => i.Giorni)
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
                Giorni = itinerario.Giorni.Select(g => new ItinerarioGiornoRequestDto
                {
                    IdItinerarioGiorno =g.IdItinerarioGiorno,
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
                Giorni = itinerarioCreateRequestDto.ItinerarioGiorni.Select(g => new ItinerarioGiorno
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
                ItinerarioGiorni = itinerario.Giorni.Select(g => new ItinerarioGiornoCreateRequestDto
                {
                    Giorno = g.Giorno,
                    Titolo = g.Titolo,
                    Descrizione = g.Descrizione
                }).ToList(),
                Partenze = itinerario.Partenze.Select(p => new PartenzaCreateRequestDto
                {

                    DataPartenza = p.DataPartenza,
                    Stato = p.Stato,
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
                .Include(i => i.Giorni)
                .Include(i => i.Partenze)
                .FirstOrDefaultAsync(i => i.IdItinerario == id);

            if (itinerario == null)
            {
                return null;
            }

            itinerario.NomeItinerario = itinerarioUpdateRequestDto.NomeItinerario;
            itinerario.Durata = itinerarioUpdateRequestDto.Durata;
            itinerario.ImmagineUrl = itinerarioUpdateRequestDto.ImmagineUrl;

            // 🔁 Aggiorna le fasce di prezzo
            foreach (var fasciaDto in itinerarioUpdateRequestDto.ItinerarioFascePrezzo)
            {
                var fasciaPrezzo = itinerario.ItinerarioFascePrezzo
                    .FirstOrDefault(fp => fp.IdItinerarioFasciaPrezzo == fasciaDto.IdItinerarioFasciaPrezzo);
                if (fasciaPrezzo != null)
                {
                    fasciaPrezzo.Prezzo = fasciaDto.Prezzo;
                }
            }

            // 🔁 Sovrascrive i giorni (puoi anche usare logica di update se preferisci)
            itinerario.Giorni.Clear();
            foreach (var giorno in itinerarioUpdateRequestDto.Giorni)
            {
                itinerario.Giorni.Add(new ItinerarioGiorno
                {
                    Giorno = giorno.Giorno,
                    Titolo = giorno.Titolo,
                    Descrizione = giorno.Descrizione,
                    IdItinerario = itinerario.IdItinerario
                });
            }

            // 🔁 Sovrascrive le partenze (facoltativo — dipende dal tuo uso)
            itinerario.Partenze.Clear();
            foreach (var partenzaDto in itinerarioUpdateRequestDto.Partenze)
            {
                itinerario.Partenze.Add(new Partenza
                {
                    DataPartenza = partenzaDto.DataPartenza,
                    Stato = partenzaDto.Stato,
                    IdItinerario = itinerario.IdItinerario
                });
            }

            await _context.SaveChangesAsync();

            return new ItinerarioUpdateRequestDto
            {
              
                NomeItinerario = itinerario.NomeItinerario,
                ImmagineUrl = itinerario.ImmagineUrl,
                Durata = itinerario.Durata,
                Giorni = itinerario.Giorni.Select(g => new ItinerarioGiornoUpdateRequestDto
                {
                    IdItinerarioGiorno = g.IdItinerarioGiorno,
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
                    IdItinerarioFasciaPrezzo = fp.IdItinerarioFasciaPrezzo,
                    IdFasciaDiPrezzo= fp.IdFasciaDiPrezzo,
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



        public async Task<List<ItinerarioGetRequestDto>> GetItinerariByNomePaeseAsync(string nomePaese)
        {
            var itinerari = await _context.Itinerari
                            .Where(i => i.Paese.Nome.ToLower() == nomePaese.ToLower()) // Confronto senza distinzione tra maiuscole e minuscole
                .Include(i => i.Paese)
                .Include(i => i.ItinerarioFascePrezzo)
                    .ThenInclude(fp => fp.FasciaDiPrezzo)
                .Include(i => i.Giorni)
                .Include(i => i.Partenze)
                .ToListAsync();

            if (itinerari == null || !itinerari.Any())
            {
                return null; // O restituisci una lista vuota, a seconda della tua logica
            }

            return itinerari.Select(itinerario => new ItinerarioGetRequestDto
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
                Giorni = itinerario.Giorni.Select(g => new ItinerarioGiornoRequestDto
                {
                    IdItinerarioGiorno = g.IdItinerarioGiorno,
                    Giorno = g.Giorno,
                    Titolo = g.Titolo,
                    Descrizione = g.Descrizione
                }).ToList(),
                Partenze = itinerario.Partenze.Select(p => new PartenzaRequestDto
                {
                    IdPartenza = p.IdPartenza,
                    DataPartenza = p.DataPartenza,
                    Stato = p.Stato
                }).ToList(),
                ItinerarioFascePrezzo = itinerario.ItinerarioFascePrezzo.Select(fp => new ItinerarioFasciaPrezzoRequestDto
                {
                    IdItinerarioFasciaPrezzo = fp.IdItinerarioFasciaPrezzo,
                    IdFasciaDiPrezzo = fp.IdFasciaDiPrezzo,
                    Prezzo = fp.Prezzo
                }).ToList()
            }).ToList();
        }
    }
}
