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
                .Include(i => i.Paese)
                .FirstOrDefaultAsync(i => i.IdItinerario == id);

            if (itinerario == null) return null;

            itinerario.NomeItinerario = itinerarioUpdateRequestDto.NomeItinerario;
            itinerario.Durata = itinerarioUpdateRequestDto.Durata;
            itinerario.ImmagineUrl = itinerarioUpdateRequestDto.ImmagineUrl;

            if (itinerarioUpdateRequestDto.Paese != null)
            {
                var paese = await _context.Paesi.FindAsync(itinerarioUpdateRequestDto.Paese.IdPaese);
                if (paese == null)
                {
                    throw new Exception($"Il paese con Id {itinerarioUpdateRequestDto.Paese.IdPaese} non esiste.");
                }

                if (paese.Nome != itinerarioUpdateRequestDto.Paese.Nome)
                {
                    paese.Nome = itinerarioUpdateRequestDto.Paese.Nome;
                }

                itinerario.Paese = paese;
            }

            // 🔁 Aggiorna le fasce di prezzo
            foreach (var fasciaDto in itinerarioUpdateRequestDto.ItinerarioFascePrezzo)
            {
                var fascia = itinerario.ItinerarioFascePrezzo
                    .FirstOrDefault(f => f.IdItinerarioFasciaPrezzo == fasciaDto.IdItinerarioFasciaPrezzo);
                if (fascia != null)
                {
                    fascia.Prezzo = fasciaDto.Prezzo;
                }
            }

            // 🔁 Sovrascrive i giorni (ok cancellare e rigenerare)
            itinerario.Giorni.Clear();
            foreach (var giornoDto in itinerarioUpdateRequestDto.Giorni)
            {
                itinerario.Giorni.Add(new ItinerarioGiorno
                {
                    Giorno = giornoDto.Giorno,
                    Titolo = giornoDto.Titolo,
                    Descrizione = giornoDto.Descrizione,
                    IdItinerario = itinerario.IdItinerario
                });
            }

            // 🔁 Aggiorna o aggiunge partenze senza rimuovere
            foreach (var partenzaDto in itinerarioUpdateRequestDto.Partenze)
            {
                var partenza = itinerario.Partenze
                    .FirstOrDefault(p => p.IdPartenza == partenzaDto.IdPartenza);

                if (partenza != null)
                {
                    partenza.DataPartenza = partenzaDto.DataPartenza;
                    partenza.Stato = partenzaDto.Stato;

                    // Aggiorna anche gli elementi del carrello associati
                    var carrelloItems = await _context.CarrelloItems
                        .Where(ci => ci.IdPartenza == partenza.IdPartenza)
                        .ToListAsync();

                    foreach (var item in carrelloItems)
                    {
                        item.Partenza.DataPartenza = partenzaDto.DataPartenza;
                        item.Partenza.Stato = partenzaDto.Stato;
                    }
                }
                else
                {
                    // Aggiunta nuova partenza
                    itinerario.Partenze.Add(new Partenza
                    {
                        DataPartenza = partenzaDto.DataPartenza,
                        Stato = partenzaDto.Stato,
                        IdItinerario = itinerario.IdItinerario
                    });
                }
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
                    Stato = p.Stato
                }).ToList(),
                ItinerarioFascePrezzo = itinerario.ItinerarioFascePrezzo.Select(fp => new ItinerarioFasciaPrezzoUpdateRequestDto
                {
                    IdItinerarioFasciaPrezzo = fp.IdItinerarioFasciaPrezzo,
                    IdFasciaDiPrezzo = fp.IdFasciaDiPrezzo,
                    Prezzo = fp.Prezzo
                }).ToList(),
                Paese = itinerario.Paese != null
                    ? new PaeseRequestDto
                    {
                        IdPaese = itinerario.Paese.IdPaese,
                        Nome = itinerario.Paese.Nome
                    }
                    : null
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
