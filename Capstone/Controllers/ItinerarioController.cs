using Capstone.DTOs.Itinerario;
using Capstone.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ItinerarioController : ControllerBase
{
    private readonly ItinerarioService _itinerarioService;
    private readonly ILogger<ItinerarioController> _logger;

    public ItinerarioController(ItinerarioService itinerarioService, ILogger<ItinerarioController> logger)
    {
        _itinerarioService = itinerarioService;
        _logger = logger;
    }

    // GET: api/itinerari
    [HttpGet]
    public async Task<ActionResult<ItinerarioGetRequestDto>> GetAllItinerari()
    {
        try
        {
            var itinerari = await _itinerarioService.GetItinerariAsync();
            return Ok(itinerari);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante il recupero degli itinerari");
            return StatusCode(500, "Errore interno del server");
        }
    }

    // GET: api/itinerari/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ItinerarioGetRequestDto>> GetItinerarioById(int id)
    {
        try
        {
            var itinerario = await _itinerarioService.GetItinerarioAsync(id);
            if (itinerario == null)
            {
                return NotFound();
            }
            return Ok(itinerario);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Errore durante il recupero dell'itinerario con ID {id}");
            return StatusCode(500, "Errore interno del server");
        }
    }

    // PUT: api/itinerari/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItinerario(int id, [FromBody] ItinerarioUpdateRequestDto itinerarioUpdateRequestDto)
    {
        try
        {
            var itinerario = await _itinerarioService.UpdateItinerarioAsync(id, itinerarioUpdateRequestDto);
            if (itinerario == null)
            {
                return NotFound();
            }
            return Ok(itinerario);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Errore durante l'aggiornamento dell'itinerario con ID {id}");
            return StatusCode(500, "Errore interno del server");
        }
    }

    // POST: api/itinerari
    [HttpPost]
    public async Task<ActionResult<ItinerarioCreateRequestDto>> CreateItinerario([FromBody] ItinerarioCreateRequestDto itinerarioCreateRequestDto)
    {
        try
        {
            var itinerario = await _itinerarioService.CreateItinerarioAsync(itinerarioCreateRequestDto);
            if (itinerario == null)
            {
                return NotFound();
            }
            return Ok(itinerario);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante la creazione dell'itinerario");
            return StatusCode(500, "Errore interno del server");
        }
    }

    // DELETE: api/itinerari/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItinerario(int id)
    {
        try
        {
            var itinerario = await _itinerarioService.GetItinerarioAsync(id);
            if (itinerario == null)
            {
                return NotFound();
            }
            await _itinerarioService.DeleteItinerarioAsync(id);
            return Ok(itinerario);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Errore durante l'eliminazione dell'itinerario con ID {id}");
            return StatusCode(500, "Errore interno del server");
        }
    }
}
