using Capstone.Data;
using Capstone.DTOs.Paese;
using Capstone.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Services
{
    public class PaeseService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PaeseService> _logger;

        public PaeseService(ApplicationDbContext context, ILogger<PaeseService> logger)
        {
            _context = context;
            _logger = logger;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        // ✅ GET ALL
        public async Task<List<PaeseRequestDto>?> GetAllPaeseAsync()
        {
            try
            {
                var paesi = await _context.Paesi.ToListAsync();

                return paesi.Select(p => new PaeseRequestDto
                {
                    IdPaese = p.IdPaese,
                    Nome = p.Nome
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

       
        public async Task<PaeseRequestDto?> GetPaeseByIdAsync(int id)
        {
            try
            {
                var paese = await _context.Paesi.FindAsync(id);
                if (paese == null) return null;

                return new PaeseRequestDto
                {
                    IdPaese = paese.IdPaese,
                    Nome = paese.Nome
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        // ✅ CREATE (POST)
        public async Task<bool> CreatePaeseAsync(AddPaeseRequestDto addPaeseRequestDto)
        {
            try
            {
                var paese = new Paese
                {
                    Nome = addPaeseRequestDto.Nome
                };

                _context.Paesi.Add(paese);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdatePaeseAsync(int id, UpdatePaeseRequestDto updatePaeseRequestDto)
        {
            try
            {
                var paese = await _context.Paesi.FindAsync(id);
                if (paese == null) return false;

                paese.Nome = updatePaeseRequestDto.Nome;

                _context.Paesi.Update(paese);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        // ✅ DELETE
        public async Task<bool> DeletePaeseAsync(int id)
        {
            try
            {
                var paese = await _context.Paesi.FindAsync(id);
                if (paese == null) return false;

                _context.Paesi.Remove(paese);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
