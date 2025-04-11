using Microsoft.EntityFrameworkCore;
using Capstone.Models;
using Capstone.Data;
using Microsoft.AspNetCore.Identity;
using Capstone.DTOs.Recensione;

public class RecensioneService
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly UserManager<ApplicationUser> _userManager;

    public RecensioneService(ApplicationDbContext context, IWebHostEnvironment env, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _env = env;
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
                TitoloItinerario = r.Itinerario.NomeItinerario
            }).FirstOrDefaultAsync();  
    }

    public async Task<Recensione> CreateAsync(RecensioneCreateRequestDto dto, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        // salva immagine se fornita
        if (dto.ImgUser != null && dto.ImgUser.Length > 0)
        {
            var fileExt = Path.GetExtension(dto.ImgUser.FileName);
            var fileName = $"{userId}_{Guid.NewGuid()}{fileExt}";
            var savePath = Path.Combine(_env.WebRootPath, "images", "users", fileName);

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
            UserId = dto.UserId,
            IdItinerario = dto.IdItinerario,
            CreatedAt = DateOnly.FromDateTime(DateTime.Today)
        };

        _context.Recensioni.Add(recensione);
        await _context.SaveChangesAsync();

        return recensione;
    }

    public async Task<bool> UpdateAsync(RecensioneUpdateRequestDto dto, string userId)
    {
        var recensione = await _context.Recensioni.FindAsync(dto.IdRecensione);

        if (recensione == null )
            return false;

        recensione.Commento = dto.Commento;
        recensione.Valutazione = dto.Valutazione;

        var user = await _userManager.FindByIdAsync(userId);

        if (dto.ImgUser != null && dto.ImgUser.Length > 0)
        {
            var fileExt = Path.GetExtension(dto.ImgUser.FileName);
            var fileName = $"{userId}_{Guid.NewGuid()}{fileExt}";
            var savePath = Path.Combine(_env.WebRootPath, "images", "users", fileName);

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

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var recensione = await _context.Recensioni.FindAsync(id);

        if (recensione == null)
            return false;

        _context.Recensioni.Remove(recensione);
        await _context.SaveChangesAsync();

        return true;
    }
}
