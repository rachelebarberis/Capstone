using Capstone.Data;
using Capstone.DTOs.FasceDiPrezzo;

using Capstone.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Services
{
    public class FasceDiPrezzoService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FasceDiPrezzoService> _logger;

        public FasceDiPrezzoService(ApplicationDbContext context, ILogger<FasceDiPrezzoService> logger)
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

        public async Task<List<FasceDiPrezzoRequestDto>?> GetAllFasceDiPrezzoAsync()
        {
            try
            {
                var fasciaDiPrezzo = await _context.FasceDiPrezzo.ToListAsync();

                return fasciaDiPrezzo.Select(p => new FasceDiPrezzoRequestDto
                {
                    IdFasciaDiPrezzo = p.IdFasciaDiPrezzo,
                    Nome = p.Nome
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }


        public async Task<FasceDiPrezzoRequestDto?> GetFasceDiPrezzoByIdAsync(int id)
        {
            try
            {
                var fasciaDiPrezzo = await _context.FasceDiPrezzo.FindAsync(id);
                if (fasciaDiPrezzo == null) return null;

                return new FasceDiPrezzoRequestDto
                {
                    IdFasciaDiPrezzo = fasciaDiPrezzo.IdFasciaDiPrezzo,
                    Nome = fasciaDiPrezzo.Nome
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

      
        public async Task<bool> CreateFasciaDiPrezzoAsync(AddFasceDiPrezzoResquestDto addFasceDiPrezzoResquestDto)
        {
            try
            {
                var fasciaDiPrezzo = new FasciaDiPrezzo
                {
                    Nome = addFasceDiPrezzoResquestDto.Nome
                };

                _context.FasceDiPrezzo.Add(fasciaDiPrezzo);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateFasciaDiPrezzoAsync(int id, UpdateFasceDiPrezzoRequestDto updateFasceDiPrezzoRequestDto)
        {
            try
            {
                var fdp = await _context.FasceDiPrezzo.FindAsync(id);
                if (fdp == null) return false;

                fdp.Nome = updateFasceDiPrezzoRequestDto.Nome;

                _context.FasceDiPrezzo.Update(fdp);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

      
        public async Task<bool> DeleteFasciaDiPrezzoAsync(int id)
        {
            try
            {
                var fdp = await _context.FasceDiPrezzo.FindAsync(id);
                if (fdp == null) return false;

                _context.FasceDiPrezzo.Remove(fdp);
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

