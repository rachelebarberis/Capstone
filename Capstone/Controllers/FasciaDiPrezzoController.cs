using Capstone.Data;
using Capstone.DTOs.FasceDiPrezzo;
using Capstone.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FasciaDiPrezzoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FasciaDiPrezzoController> _logger;
        private readonly FasceDiPrezzoService _fasceDiPrezzoService;

        public FasciaDiPrezzoController(ApplicationDbContext context, FasceDiPrezzoService fasceDiPrezzoService, ILogger<FasciaDiPrezzoController> logger)
        {
            _context = context;
            _fasceDiPrezzoService = fasceDiPrezzoService;
            _logger = logger;
        }

        // GET: api/fasciadiprezzo
        [HttpGet]
        public async Task<IActionResult> GetAllFDP()
        {
            try
            {
                var fdp = await _fasceDiPrezzoService.GetAllFasceDiPrezzoAsync();
                return Ok(fdp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle fasce di prezzo");
                return StatusCode(500, "Errore interno del server");
            }
        }

        // GET: api/fasciadiprezzo/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFDPById(int id)
        {
            try
            {
                var fdp = await _fasceDiPrezzoService.GetFasceDiPrezzoByIdAsync(id);
                if (fdp == null)
                    return NotFound($"Fascia di prezzo con ID {id} non trovato.");

                return Ok(fdp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante il recupero della fascia di prezzo con ID {id}");
                return StatusCode(500, "Errore interno del server");
            }
        }

        // POST: api/fasciadiprezzo
        [HttpPost]
        public async Task<IActionResult> CreateFasciaDiPrezzo([FromBody] AddFasceDiPrezzoResquestDto addFasceDiPrezzoResquestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _fasceDiPrezzoService.CreateFasciaDiPrezzoAsync(addFasceDiPrezzoResquestDto);
                if (!result)
                    return StatusCode(500, "Errore durante la creazione della fascia di prezzo.");

                return Ok("Fascia di prezzo creata con successo.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione della fascia di prezzo");
                return StatusCode(500, "Errore interno del server");
            }
        }

        // PUT: api/fasciadiprezzo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFasciaDiPrezzo(int id, [FromBody] UpdateFasceDiPrezzoRequestDto updateFasceDiPrezzoRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _fasceDiPrezzoService.UpdateFasciaDiPrezzoAsync(id, updateFasceDiPrezzoRequestDto);
                if (!result)
                    return NotFound($"Fascia di prezzo con ID {id} non trovata o aggiornamento fallito.");

                return Ok("Fascia di prezzo aggiornata con successo.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante l'aggiornamento della fascia di prezzo con ID {id}");
                return StatusCode(500, "Errore interno del server");
            }
        }

        // DELETE: api/fasciadiprezzo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFasciaDiPrezzo(int id)
        {
            try
            {
                var result = await _fasceDiPrezzoService.DeleteFasciaDiPrezzoAsync(id);
                if (!result)
                    return NotFound($"Fascia di prezzo con ID {id} non trovata o eliminazione fallita.");

                return Ok("Fascia di prezzo eliminata con successo.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante l'eliminazione della fascia di prezzo con ID {id}");
                return StatusCode(500, "Errore interno del server");
            }
        }
    }
}
