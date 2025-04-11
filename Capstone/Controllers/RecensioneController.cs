using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Capstone.DTOs.Recensione;
using Capstone.Models;


[ApiController]
[Route("api/[controller]")]
public class RecensioniController : ControllerBase
{
    private readonly RecensioneService _service;

    public RecensioniController(RecensioneService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var recensioni = await _service.GetAllAsync();
        return Ok(recensioni);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var recensione = await _service.GetByIdAsync(id);
        if (recensione == null)
            return NotFound();

        return Ok(recensione);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] RecensioneCreateRequestDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var recensione = await _service.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id = recensione.IdRecensione }, recensione);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] RecensioneUpdateRequestDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _service.UpdateAsync(dto, userId);

        if (!result)
            return Forbid();


        return Ok(result); ;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _service.DeleteAsync(id, userId);

        if (!result)
            return Forbid();


        return Ok(result);
    }
}
