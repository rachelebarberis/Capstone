using Capstone.Data;
using Capstone.DTOs.Paese;
using Capstone.Services;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaeseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PaeseController> _logger;
        private readonly PaeseService _paeseService;

        public PaeseController(ApplicationDbContext context, PaeseService paeseService, ILogger<PaeseController> logger)
        {
            _context = context;
            _paeseService = paeseService;
            _logger = logger;
        }

        // GET: api/paese
        [HttpGet]
        public async Task<IActionResult> GetAllPaese()
        {
            try
            {
                var paesi = await _paeseService.GetAllPaeseAsync();
                return Ok(paesi);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei Paesi");
                return StatusCode(500, "Errore interno del server");
            }
        }

        // GET: api/paese/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaeseById(int id)
        {
            var paese = await _paeseService.GetPaeseByIdAsync(id);
            if (paese == null)
                return NotFound($"Paese con ID {id} non trovato.");

            return Ok(paese);
        }

        // POST: api/paese
        [HttpPost]
        public async Task<IActionResult> CreatePaese([FromBody] AddPaeseRequestDto addPaeseRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _paeseService.CreatePaeseAsync(addPaeseRequestDto);
            if (!result)
                return StatusCode(500, "Errore durante la creazione del paese.");

            return Ok("Paese creato con successo.");
        }

        // PUT: api/paese/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaese(int id, [FromBody] UpdatePaeseRequestDto updatePaeseRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _paeseService.UpdatePaeseAsync(id, updatePaeseRequestDto);
            if (!result)
                return NotFound($"Paese con ID {id} non trovato o aggiornamento fallito.");

            return Ok("Paese aggiornato con successo.");
        }

        // DELETE: api/paese/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaese(int id)
        {
            var result = await _paeseService.DeletePaeseAsync(id);
            if (!result)
                return NotFound($"Paese con ID {id} non trovato o eliminazione fallita.");

            return Ok("Paese eliminato con successo.");
        }
    }
}
