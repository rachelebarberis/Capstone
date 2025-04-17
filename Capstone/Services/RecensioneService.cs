using Microsoft.EntityFrameworkCore;
using Capstone.Models;
using Capstone.Data;
using Microsoft.AspNetCore.Identity;
using Capstone.DTOs.Recensione;
using System.IO;

public class RecensioneService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public RecensioneService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<List<RecensioneGetRequestDto>> GetAllAsync()
    {
        return await _context.Recensioni
            .Include(r => r.User)
            .Include(r => r.Itinerario)
            .Select(r => new RecensioneGetRequestDto
            {
                IdRecensione = r.IdRecensione,
                CreatedAt = r.CreatedAt,
                Commento = r.Commento,
                Valutazione = r.Valutazione,
                UserId = r.UserId,
                NomeUtente = r.User.FirstName + " " + r.User.LastName,
                ImgUserPath = r.User.ImgUserPath,
                Email= r.User.Email,
                IdItinerario = r.IdItinerario,
                TitoloItinerario = r.Itinerario.NomeItinerario
            }).ToListAsync();
    }

    public async Task<RecensioneGetRequestDto?> GetByIdAsync(int idRecensione)
    {
        return await _context.Recensioni
            .Include(r => r.User)
            .Include(r => r.Itinerario)
            .Where(r => r.IdRecensione == idRecensione)
            .Select(r => new RecensioneGetRequestDto
            {
                IdRecensione = r.IdRecensione,
                CreatedAt = r.CreatedAt,
                Commento = r.Commento,
                Valutazione = r.Valutazione,
                UserId = r.UserId,
                NomeUtente = r.User.FirstName + " " + r.User.LastName,
                ImgUserPath = r.User.ImgUserPath,
                IdItinerario = r.IdItinerario,
                TitoloItinerario = r.Itinerario.NomeItinerario,
                Email = r.User.Email
            }).FirstOrDefaultAsync();
    }

    public async Task<Recensione> CreateAsync(RecensioneCreateRequestDto dto, string userEmail)
    {
        // Recupera l'utente tramite l'email
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        if (user == null)
            throw new ArgumentException("User not found.");

        // Salva immagine se fornita
        if (dto.ImgUser != null && dto.ImgUser.Length > 0)
        {
            var fileExt = Path.GetExtension(dto.ImgUser.FileName);
            var fileName = $"{userEmail}_{Guid.NewGuid()}{fileExt}";

            // Usa un percorso di salvataggio relativo per le immagini
            var savePath = Path.Combine("wwwroot", "images", "users", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await dto.ImgUser.CopyToAsync(stream);
            }

            user.ImgUserPath = $"images/users/{fileName}";
            await _userManager.UpdateAsync(user);
        }

        var recensione = new Recensione
        {
            Commento = dto.Commento,
            Valutazione = dto.Valutazione,
            UserId = user.Id, // Associa la recensione all'utente tramite userId
            IdItinerario = dto.IdItinerario,
            CreatedAt = DateOnly.FromDateTime(DateTime.Today)
        };

        _context.Recensioni.Add(recensione);
        await _context.SaveChangesAsync();

        return recensione;
    }

    public async Task<bool> UpdateAsync(RecensioneUpdateRequestDto dto, string userEmail)
    {
        var recensione = await _context.Recensioni.FindAsync(dto.IdRecensione);

        if (recensione == null)
            return false;

        // Confronta con l'email dell'utente
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        if (recensione.UserId != user.Id)
            return false;  // Non puoi modificare recensioni di altri utenti

        recensione.Commento = dto.Commento;
        recensione.Valutazione = dto.Valutazione;

        // Salva immagine se fornita
        if (dto.ImgUser != null && dto.ImgUser.Length > 0)
        {
            var fileExt = Path.GetExtension(dto.ImgUser.FileName);
            var fileName = $"{userEmail}_{Guid.NewGuid()}{fileExt}";

            // Usa un percorso di salvataggio relativo per le immagini
            var savePath = Path.Combine("wwwroot", "images", "users", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await dto.ImgUser.CopyToAsync(stream);
            }

            user.ImgUserPath = $"images/users/{fileName}";
            await _userManager.UpdateAsync(user);
        }

        _context.Recensioni.Update(recensione);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id, string userEmail)
    {
        var recensione = await _context.Recensioni.FindAsync(id);

        if (recensione == null)
            return false;

        // Confronta con l'email dell'utente
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        if (recensione.UserId != user.Id)
            return false;  // Non puoi eliminare recensioni di altri utenti

        _context.Recensioni.Remove(recensione);
        await _context.SaveChangesAsync();

        return true;
    }
}
