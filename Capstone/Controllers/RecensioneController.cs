using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Capstone.DTOs.Recensione;
using Capstone.Models;
using Capstone.Services;

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
    [Authorize]
    public async Task<IActionResult> Create([FromForm] RecensioneCreateRequestDto dto)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);  // Usa l'email

        if (string.IsNullOrEmpty(userEmail))
            return Unauthorized();

        var recensione = await _service.CreateAsync(dto, userEmail);
        return CreatedAtAction(nameof(GetById), new { id = recensione.IdRecensione }, recensione);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromForm] RecensioneUpdateRequestDto dto)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);  // Usa l'email

        if (string.IsNullOrEmpty(userEmail))
            return Unauthorized();

        var result = await _service.UpdateAsync(dto, userEmail);

        if (!result)
            return Forbid();  // Forbid = 403

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);  // Usa l'email

        if (string.IsNullOrEmpty(userEmail))
            return Unauthorized();

        var result = await _service.DeleteAsync(id, userEmail);

        if (!result)
            return Forbid();

        return Ok(result);
    }
}
