using Microsoft.AspNetCore.Mvc;
using Capstone.DTOs.Carrello;
using Capstone.Services;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    public class CarrelloController : ControllerBase
    {
        private readonly CarrelloService _carrelloService;

        public CarrelloController(CarrelloService carrelloService)
        {
            _carrelloService = carrelloService;
        }

        // Modificato per utilizzare l'email
        [HttpGet("{email}")]
        public async Task<IActionResult> GetCarrello(string email)
        {
            var result = await _carrelloService.GetCarrelloByEmailAsync(email); // Modificato nel servizio
            return result == null ? NotFound() : Ok(result);
        }

        // Modificato per utilizzare l'email invece dell'ID
        [HttpDelete("clear/{email}")]
        public async Task<IActionResult> ClearCarrello(string email)
        {
            var success = await _carrelloService.ClearCarrelloAsync(email); // Modificato nel servizio
            return success ? NoContent() : NotFound();
        }

        // Metodo per creare il carrello, utilizza l'email nel DTO
        [HttpPost]
        public async Task<IActionResult> CreateCarrello([FromBody] CarrelloCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _carrelloService.CreateCarrelloAsync(dto);
            return Ok(result);
        }

        // Metodo per aggiornare l'item nel carrello
        [HttpPut("item/{itemId}")]
        public async Task<IActionResult> UpdateCarrelloItem(int itemId, [FromBody] CarrelloItemUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _carrelloService.UpdateCarrelloItemAsync(itemId, dto);
            return result == null ? NotFound() : Ok(result);
        }

        // Metodo per rimuovere un item dal carrello
        [HttpDelete("item/{itemId}")]
        public async Task<IActionResult> RemoveItem(int itemId)
        {
            var success = await _carrelloService.RemoveItemAsync(itemId);
            return success ? NoContent() : NotFound();
        }
    }
}
