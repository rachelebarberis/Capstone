using Capstone.DTOs.Recensione;
using Capstone.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecensioneController : ControllerBase
    {
        private readonly RecensioneService _recensioneService;
        private readonly ILogger<RecensioneController> _logger;

        public RecensioneController(RecensioneService recensioneService, ILogger<RecensioneController> logger)
        {
            _recensioneService = recensioneService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<List<RecensioneGetRequestDto>>> GetRecensioniAll()
        {
            try
            {
                var recensioni = await _recensioneService.GetRecensioniAll();
                return Ok(recensioni);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle recensioni");
                return StatusCode(500, "Errore interno del server");
            }
        }

        // GET: api/recensioni/{idItinerario}
        [HttpGet("itinerario/{idItinerario}")]
        public async Task<ActionResult<List<RecensioneGetRequestDto>>> GetRecensioniByItinerario(int idItinerario)
        {
            try
            {
                var recensioni = await _recensioneService.GetRecensioniByItinerarioAsync(idItinerario);
                return Ok(recensioni);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle recensioni");
                return StatusCode(500, "Errore interno del server");
            }
        }

        // GET: api/recensioni/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RecensioneGetRequestDto>> GetRecensioneById(int id)
        {
            try
            {
                var recensione = await _recensioneService.GetRecensioneByIdAsync(id);
                if (recensione == null)
                {
                    return NotFound();
                }
                return Ok(recensione);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante il recupero della recensione con ID {id}");
                return StatusCode(500, "Errore interno del server");
            }
        }

        // POST: api/recensioni
        [HttpPost]
        public async Task<ActionResult<RecensioneCreateRequestDto>> CreateRecensione([FromBody] RecensioneCreateRequestDto recensioneCreateRequestDto)
        {
            try
            {
                var recensione = await _recensioneService.CreateRecensioneAsync(recensioneCreateRequestDto);
                return Ok(recensione);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione della recensione");
                return StatusCode(500, "Errore interno del server");
            }
        }

        // PUT: api/recensioni/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecensione(int id, [FromBody] RecensioneUpdateRequestDto recensioneUpdateRequestDto)
        {
            try
            {
                var recensione = await _recensioneService.UpdateRecensioneAsync(id, recensioneUpdateRequestDto);
                if (recensione == null)
                {
                    return NotFound();
                }
                return Ok(recensione);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante l'aggiornamento della recensione con ID {id}");
                return StatusCode(500, "Errore interno del server");
            }
        }

        // DELETE: api/recensioni/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecensione(int id)
        {
            try
            {
                await _recensioneService.DeleteRecensioneAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante l'eliminazione della recensione con ID {id}");
                return StatusCode(500, "Errore interno del server");
            }
        }
    }
}
