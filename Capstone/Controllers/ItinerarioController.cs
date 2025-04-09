using Capstone.DTOs.Itinerario;
using Capstone.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ItinerarioController : ControllerBase
{
    private readonly ItinerarioService _itinerarioService;

    public ItinerarioController(ItinerarioService itinerarioService)
    {
        _itinerarioService = itinerarioService;
    }

    // GET: api/itinerari
    [HttpGet]
    public async Task<ActionResult<ItinerarioGetRequestDto>>GetAllItinerari()
    {
        var itinerari = await _itinerarioService.GetItinerariAsync();
        return Ok(itinerari);
    }

    // GET: api/itinerari/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ItinerarioGetRequestDto>> GetItinerarioById(int id)
    {
        var itinerario = await _itinerarioService.GetItinerarioAsync(id);
        if (itinerario == null)
        {
            return NotFound();
        }
        return Ok(itinerario);
    }

    // PUT: api/itinerari/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItinerario(int id, [FromBody] ItinerarioUpdateRequestDto itinerarioUpdateRequestDto)
    {
      

        var itinerario = await _itinerarioService.UpdateItinerarioAsync(id, itinerarioUpdateRequestDto);
        if (itinerario == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    // POST: api/itinerari
    [HttpPost]
    public async Task<ActionResult<ItinerarioCreateRequestDto>> CreateItinerario([FromBody] ItinerarioCreateRequestDto itinerarioCreateRequestDto)
    {
        var itinerario = await _itinerarioService.CreateItinerarioAsync(itinerarioCreateRequestDto);
        if (itinerario == null)
        {
            return NotFound();
        }
        return NoContent();

    }

    // DELETE: api/itinerari/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItinerario(int id)
    {
        var itinerario = await _itinerarioService.GetItinerarioAsync(id);
        if (itinerario == null)
        {
            return NotFound();
        }
        await _itinerarioService.DeleteItinerarioAsync(id);
        return NoContent();
    }
}
