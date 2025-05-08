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

      
        [HttpGet("{email}")]
        public async Task<IActionResult> GetCarrello(string email)
        {
            var result = await _carrelloService.GetCarrelloByEmailAsync(email); 
            return result == null ? NotFound() : Ok(result);
        }

       
        [HttpDelete("clear/{email}")]
        public async Task<IActionResult> ClearCarrello(string email)
        {
            var success = await _carrelloService.ClearCarrelloAsync(email); 
            return success ? NoContent() : NotFound();
        }

     
        [HttpPost]
        public async Task<IActionResult> CreateCarrello([FromBody] CarrelloCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _carrelloService.CreateCarrelloAsync(dto);
            return Ok(result);
        }

     
        [HttpPut("item/{itemId}")]
        public async Task<IActionResult> UpdateCarrelloItem(int itemId, [FromBody] CarrelloItemUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _carrelloService.UpdateCarrelloItemAsync(itemId, dto);
            return result == null ? NotFound() : Ok(result);
        }


        [HttpDelete("item/{itemId}")]
        public async Task<IActionResult> RemoveItem(int itemId)
        {
            var success = await _carrelloService.RemoveItemAsync(itemId);
            return success ? NoContent() : NotFound();
        }
    }
}
