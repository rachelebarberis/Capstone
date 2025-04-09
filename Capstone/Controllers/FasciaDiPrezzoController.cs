using Capstone.Data;
using Capstone.DTOs.FasceDiPrezzo;
using Capstone.DTOs.Paese;
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
                _logger.LogError(ex, "Errore durante il recupero");
                return StatusCode(500, "Errore interno del server");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetFDPById(int id)
        {
            var fdp = await _fasceDiPrezzoService.GetFasceDiPrezzoByIdAsync(id);
            if (fdp == null)
                return NotFound($"Fascia di prezzo con ID {id} non trovato.");

            return Ok(fdp);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaese([FromBody] AddFasceDiPrezzoResquestDto addfasceDiPrezzoResquestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _fasceDiPrezzoService.CreateFasciaDiPrezzoAsync(addfasceDiPrezzoResquestDto);
            if (!result)
                return StatusCode(500, "Errore durante la creazione del paese.");

            return Ok("Fascia di prezzo creata con successo.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaese(int id, [FromBody] UpdateFasceDiPrezzoRequestDto updateFasceDiPrezzoRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _fasceDiPrezzoService.UpdateFasciaDiPrezzoAsync(id, updateFasceDiPrezzoRequestDto);
            if (!result)
                return NotFound($"Fascia di prezzo con ID {id} non trovato o aggiornamento fallito.");

            return Ok("Fascia di prezzo aggiornata con successo.");
        }

        // DELETE: api/paese/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaese(int id)
        {
            var result = await _fasceDiPrezzoService.DeleteFasciaDiPrezzoAsync(id);
            if (!result)
                return NotFound($"Fascia di prezzo con ID {id} non trovato o eliminazione fallita.");

            return Ok("Fascia di prezzo eliminata con successo.");
        }
    }
}
