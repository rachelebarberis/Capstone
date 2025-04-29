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

    // PUT: api/itinerari/{id}

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItinerario(int id, [FromBody] ItinerarioUpdateRequestDto itinerarioUpdateRequestDto)
    {
        // Controlla se i dati inviati sono validi
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();

            // Logga gli errori di validazione
            Console.WriteLine("Errore di validazione: ");
            errors.ForEach(error => Console.WriteLine(error));

            return BadRequest(errors);  // Restituisce gli errori di validazione al client
        }

        // Logga i dati ricevuti per la verifica
        Console.WriteLine($"Aggiornamento itinerario con ID: {id}");
        Console.WriteLine($"Nome itinerario: {itinerarioUpdateRequestDto.NomeItinerario}");
        Console.WriteLine($"Durata: {itinerarioUpdateRequestDto.Durata}");
        Console.WriteLine($"Immagine URL: {itinerarioUpdateRequestDto.ImmagineUrl}");

        // Verifica se i dati di giorni, partenze, fasce di prezzo sono presenti
        if (itinerarioUpdateRequestDto.Giorni != null && itinerarioUpdateRequestDto.Giorni.Any())
        {
            Console.WriteLine($"Numero di giorni: {itinerarioUpdateRequestDto.Giorni.Count}");
        }
        else
        {
            Console.WriteLine("Nessun giorno presente.");
        }

        if (itinerarioUpdateRequestDto.Partenze != null && itinerarioUpdateRequestDto.Partenze.Any())
        {
            Console.WriteLine($"Numero di partenze: {itinerarioUpdateRequestDto.Partenze.Count}");
        }
        else
        {
            Console.WriteLine("Nessuna partenza presente.");
        }

        // Passa la richiesta al servizio
        var result = await _itinerarioService.UpdateItinerarioAsync(id, itinerarioUpdateRequestDto);

        if (result == null)
        {
            Console.WriteLine("Itinerario non trovato.");
            return NotFound($"Itinerario con ID {id} non trovato.");
        }

        // Ritorna l'itinerario aggiornato
        return Ok(result);
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

    // GET: api/itinerari/paese/nome/{nomePaese}
    [HttpGet("paese/nome/{nomePaese}")]
    public async Task<ActionResult<ItinerarioGetRequestDto>> GetItinerariByNomePaese(string nomePaese)
    {
        try
        {
            var itinerari = await _itinerarioService.GetItinerariByNomePaeseAsync(nomePaese);
            if (itinerari == null || !itinerari.Any())
            {
                return NotFound($"Nessun itinerario trovato per il paese con nome {nomePaese}");
            }
            return Ok(itinerari);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Errore durante il recupero degli itinerari per il paese con nome {nomePaese}");
            return StatusCode(500, "Errore interno del server");
        }
    }


}
