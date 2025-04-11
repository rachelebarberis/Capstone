using Capstone.DTOs.Carrello;
using Capstone.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrelloController : ControllerBase
    {
        private readonly CarrelloService _carrelloService;
        private readonly ILogger<CarrelloController> _logger;

        public CarrelloController(CarrelloService carrelloService, ILogger<CarrelloController> logger)
        {
            _carrelloService = carrelloService;
            _logger = logger;
        }

        // Crea un carrello per un utente
        [HttpPost("create")]
        public async Task<ActionResult<CarrelloDto>> CreateCart([FromBody] CarrelloCreateDto carrelloCreateDto)
        {
            try
            {
                if (carrelloCreateDto == null)
                {
                    _logger.LogWarning("Richiesta di creazione carrello fallita: i dati del carrello sono nulli.");
                    return BadRequest("I dati del carrello non sono validi.");
                }

                _logger.LogInformation("Creazione di un nuovo carrello per l'utente {UserId}.", carrelloCreateDto.UserId);

                var result = await _carrelloService.CreaCarrelloAsync(carrelloCreateDto);

                if (result == null)
                {
                    _logger.LogError("Errore nella creazione del carrello per l'utente {UserId}.", carrelloCreateDto.UserId);
                    return BadRequest("Errore nella creazione del carrello.");
                }

                _logger.LogInformation("Carrello creato con successo per l'utente {UserId}.", carrelloCreateDto.UserId);
                return CreatedAtAction(nameof(CreateCart), new { id = result.IdCarrello }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore imprevisto durante la creazione del carrello.");
                return StatusCode(500, "Errore interno del server.");
            }
        }

        // Aggiungi un articolo al carrello
        [HttpPost("add/{userId}")]
        public async Task<ActionResult<CarrelloItemDto>> AggiungiAlCarrello(string userId, [FromBody] CarrelloItemCreateDto carrelloItemCreateDto)
        {
            try
            {
                if (carrelloItemCreateDto == null)
                {
                    _logger.LogWarning("Richiesta di aggiungere articolo fallita: i dati dell'articolo del carrello sono nulli.");
                    return BadRequest("I dati dell'articolo del carrello non sono validi.");
                }

                _logger.LogInformation("Aggiunta di un articolo al carrello dell'utente {UserId}.", userId);

                var result = await _carrelloService.AggiungiAlCarrelloAsync(userId, carrelloItemCreateDto);

                if (result == null)
                {
                    _logger.LogWarning("Carrello non trovato per l'utente {UserId}.", userId);
                    return NotFound("Carrello non trovato.");
                }

                _logger.LogInformation("Articolo aggiunto al carrello per l'utente {UserId}.", userId);
                return CreatedAtAction(nameof(AggiungiAlCarrello), new { id = result.IdCarrelloItem }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore imprevisto durante l'aggiunta dell'articolo al carrello.");
                return StatusCode(500, "Errore interno del server.");
            }
        }

        // Aggiorna un articolo nel carrello
        [HttpPut("update/{idCarrelloItem}")]
        public async Task<ActionResult<CarrelloItemDto>> AggiornaCarrelloItem(int idCarrelloItem, [FromBody] CarrelloItemUpdateDto carrelloItemUpdateDto)
        {
            try
            {
                if (carrelloItemUpdateDto == null)
                {
                    _logger.LogWarning("Richiesta di aggiornamento articolo fallita: i dati dell'articolo del carrello sono nulli.");
                    return BadRequest("I dati dell'articolo del carrello non sono validi.");
                }

                _logger.LogInformation("Aggiornamento dell'articolo {CarrelloItemId} nel carrello.", idCarrelloItem);

                var result = await _carrelloService.AggiornaCarrelloItemAsync(idCarrelloItem, carrelloItemUpdateDto);

                if (result == null)
                {
                    _logger.LogWarning("Articolo del carrello con ID {CarrelloItemId} non trovato.", idCarrelloItem);
                    return NotFound("Articolo del carrello non trovato.");
                }

                _logger.LogInformation("Articolo {CarrelloItemId} aggiornato con successo.", idCarrelloItem);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore imprevisto durante l'aggiornamento dell'articolo del carrello.");
                return StatusCode(500, "Errore interno del server.");
            }
        }

        // Rimuovi un articolo dal carrello
        [HttpDelete("delete/{idCarrelloItem}")]
        public async Task<ActionResult> RimuoviDalCarrello(int idCarrelloItem)
        {
            try
            {
                _logger.LogInformation("Rimozione dell'articolo {CarrelloItemId} dal carrello.", idCarrelloItem);

                var success = await _carrelloService.RimuoviDalCarrelloAsync(idCarrelloItem);

                if (!success)
                {
                    _logger.LogWarning("Articolo del carrello con ID {CarrelloItemId} non trovato.", idCarrelloItem);
                    return NotFound("Articolo del carrello non trovato.");
                }

                _logger.LogInformation("Articolo {CarrelloItemId} rimosso dal carrello con successo.", idCarrelloItem);
                return NoContent();  // Operazione completata con successo, ma senza contenuto da restituire
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore imprevisto durante la rimozione dell'articolo dal carrello.");
                return StatusCode(500, "Errore interno del server.");
            }
        }
    }
}
