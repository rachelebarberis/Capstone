
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
using Capstone.Models;
    using Capstone.Data; 
    using System.Linq;
    using System.Threading.Tasks;
using Capstone.Services;


namespace Capstone.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ItinerarioController : ControllerBase
        {
            private readonly ApplicationDbContext _context;
        

            public ItinerarioController(ApplicationDbContext context)
            {

                _context = context;
        
        }

      

        // GET: api/itinerari
        [HttpGet]
            public async Task<ActionResult> GetItinerari()
            {
            var itinerari = _context.Itinerari
      .Include(i => i.Paese)
       .Include(i => i.Partenze)// Include Paese
      .Include(i => i.ItinerarioGiorni)  // Include i giorni dell'itinerario
      .Include(i => i.FasceDiPrezzo)  // Include la relazione tra Itinerario e FasciaDiPrezzo
      .ThenInclude(fp => fp.FasciaDiPrezzo) // Include la FasciaDiPrezzo effettiva (attraverso la relazione)
      .Select(i => new
      {
          i.IdItinerario,
          i.NomeItinerario,
          PaeseNome = i.Paese.Nome,
          Giorni = i.ItinerarioGiorni.Select(g => new
          {
              g.Titolo,
              g.Descrizione  // Descrizione è una proprietà di ItinerarioGiorno
          }).ToList(),
          FasceDiPrezzo = i.FasceDiPrezzo.Select(fp => new
          {
              NomeFascia = fp.FasciaDiPrezzo.Nome,  // Nome della Fascia di Prezzo
              fp.Prezzo  // Prezzo associato alla Fascia di Prezzo
          }).ToList(),
          Partenza = i.Partenze.Select(p => new
          {
              p.DataPartenza
          }).ToList(),
      })
      .ToList();




            return Ok(itinerari);
            }
        }
    }


